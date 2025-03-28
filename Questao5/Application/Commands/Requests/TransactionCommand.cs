using MediatR;
using Questao5.Application.Dto;

namespace Questao5.Application.Commands.Requests;

public class TransactionCommand : IRequest<TransactionDto>
{
    public Guid RequestId { get; set; }
    public Guid CurrentAccountId { get; set; }
    public decimal Value { get; set; }
    public string Type { get; set; }
}
