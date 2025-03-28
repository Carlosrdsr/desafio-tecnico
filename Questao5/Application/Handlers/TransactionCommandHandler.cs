using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Dto;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Extensions;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers;

public class TransactionCommandHandler : IRequestHandler<TransactionCommand, TransactionDto>
{
    private readonly ICurrentAccountRepository _currentAccountRepository;
    private readonly IMovementRepository _movementRepository;
    private readonly IIdempotencyRepository _idempotencyRepository;
    private readonly Serilog.ILogger _logger;

    public TransactionCommandHandler(
        ICurrentAccountRepository currentAccountRepository,
        IMovementRepository movementRepository,
        IIdempotencyRepository idempotencyRepository,
        Serilog.ILogger logger
        )
    {
        _currentAccountRepository = currentAccountRepository;
        _movementRepository = movementRepository;
        _idempotencyRepository = idempotencyRepository;
        _logger = logger;
    }

    public async Task<TransactionDto> Handle(TransactionCommand request, CancellationToken cancellationToken)
    {
        var idempotency = await _idempotencyRepository.GetIdempotencyByIdAsync(request.RequestId);
        if (idempotency != null)
        {
            _logger.Information("Idempotencia já existente.");
            return new TransactionDto(idempotency.Result);
        }

        _logger.Information("Verificando conta corrente.");
        var contaCorrente = await _currentAccountRepository.GetAccountByIdAsync(request.CurrentAccountId.ToString());
        if (contaCorrente == null || !contaCorrente.Ativo)
        {
            _logger.Error("Conta corrente não encontrada ou inativa");
            throw new BadRequestException("Conta corrente não encontrada ou inativa", "INVALID_ACCOUNT");
        }

        if (request.Value <= 0)
        {
            _logger.Error("Valor deve ser positivo.");
            throw new BadRequestException("Valor deve ser maior que zero.", "INVALID_VALUE");
        }

        if (request?.Type?.ToMovementType() != EMovementType.DEBITO && request?.Type?.ToMovementType() != EMovementType.CREDITO)
        {
            _logger.Error("Tipo de movimento inválido.");
            throw new BadRequestException("Tipo de movimento deve ser 'C' ou 'D'", "INVALID_TYPE");
        }

        var movement = new Movement(
            request.CurrentAccountId.ToString(),
            request.Type.ToMovementType(),
            request.Value
        );

        await _movementRepository.AddAsync(movement);
        _logger.Information("inserido na tabela movimento.");

        var registerIdempotency = new Idempotency(
            request.RequestId,
            JsonConvert.SerializeObject(request),
            movement.Id
        );
        await _idempotencyRepository.AddAsync(registerIdempotency);
        _logger.Information("inserido na tabela idempotencia.");

        return new TransactionDto(movement.Id);
    }
}
