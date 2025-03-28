using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Interfaces
{
    public interface IIdempotencyRepository
    {
        Task AddAsync(Idempotency registerIdempotency);
        Task<Idempotency> GetIdempotencyByIdAsync(Guid requestId);
    }
}
