using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Test.Helper
{
    public class MovementRepositoryTest : IMovementRepository
    {
        public Task AddAsync(Movement movement)
        {
            return null;
        }

        public async Task<decimal> GetSumByTypeAsync(string currentAccountId, EMovementType movementType)
        {
            return 10;
        }
    }
}
