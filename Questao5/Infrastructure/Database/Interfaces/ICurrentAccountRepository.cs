using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Interfaces;

public interface ICurrentAccountRepository
{
    Task<CurrentAccount> GetAccountByIdAsync(string id);
}
