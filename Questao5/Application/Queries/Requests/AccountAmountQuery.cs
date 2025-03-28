using MediatR;
using Questao5.Application.Dto;

namespace Questao5.Application.Queries.Requests
{
    public class AccountAmountQuery : IRequest<AccountAmountDto>
    {
        public string CurrentAccountId { get; private set; }

        public AccountAmountQuery(string currentAccountId)
        {
            CurrentAccountId = currentAccountId;
        }
    }
}
