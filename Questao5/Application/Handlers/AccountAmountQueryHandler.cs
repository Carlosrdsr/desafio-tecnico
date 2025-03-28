using MediatR;
using Questao5.Application.Dto;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers;

public class AccountAmountQueryHandler : IRequestHandler<AccountAmountQuery, AccountAmountDto>
{
    private readonly ICurrentAccountRepository _currentAccountRepository;
    private readonly IMovementRepository _movementRepository;
    private readonly Serilog.ILogger _logger;

    public AccountAmountQueryHandler(ICurrentAccountRepository currentAccountRepository,
        IMovementRepository movementRepository, Serilog.ILogger logger)
    {
        _currentAccountRepository = currentAccountRepository;
        _movementRepository = movementRepository;
        _logger = logger;
    }

    public async Task<AccountAmountDto> Handle(AccountAmountQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("Verificando conta corrente.");
        var contaCorrente = await _currentAccountRepository.GetAccountByIdAsync(request.CurrentAccountId);
        if (contaCorrente == null)
        {
            _logger.Error("Conta corrente não encontrada.");
            throw new BadRequestException("Conta corrente não encontrada", "INVALID_ACCOUNT");
        }

        if (!contaCorrente.Ativo)
        {
            _logger.Error("Conta corrente está inativa.");
            throw new BadRequestException("Conta corrente está inativa", "INACTIVE_ACCOUNT");
        }

        _logger.Information("Verificando saldo da conta.");
        decimal creditos = await _movementRepository.GetSumByTypeAsync(request.CurrentAccountId, EMovementType.CREDITO);
        decimal debitos = await _movementRepository.GetSumByTypeAsync(request.CurrentAccountId, EMovementType.DEBITO);
        var saldo = creditos - debitos;

        return new AccountAmountDto(
            contaCorrente.Numero,
            contaCorrente.Nome,
            DateTime.Now,
            saldo
        );
    }
}
