using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Database.Interfaces;

public interface IMovementRepository
{
    Task AddAsync(Movement movement);
    Task<decimal> GetSumByTypeAsync(string currentAccountId, EMovementType movementType);
}
