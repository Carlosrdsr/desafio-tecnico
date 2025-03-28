using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Test.Helper;

public class CurrentAccountRepositoryTest : ICurrentAccountRepository
{
    public async Task<CurrentAccount> GetAccountByIdAsync(string id)
    {
        if (id.Length < 36)
            return null;

        return new CurrentAccount();
    }
}
