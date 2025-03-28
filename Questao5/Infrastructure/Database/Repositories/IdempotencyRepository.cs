using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class IdempotencyRepository : IIdempotencyRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        public IdempotencyRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task AddAsync(Idempotency registerIdempotency)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var sql = @"INSERT INTO Idempotencia (chave_idempotencia, Requisicao, Resultado) 
                        VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)";

            var parameters = new
            {
                ChaveIdempotencia = registerIdempotency.IdempotencyKey.ToString(),
                Requisicao = registerIdempotency.Request,
                Resultado = registerIdempotency.Result
            };

            await connection.ExecuteAsync(sql, parameters);
        }

        public async Task<Idempotency> GetIdempotencyByIdAsync(Guid requestId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var sql = @"SELECT 
                            chave_idempotencia, Requisicao, Resultado 
                         FROM Idempotencia 
                        WHERE chave_idempotencia = @ChaveIdempotencia";

            var parameters = new
            {
                ChaveIdempotencia = requestId.ToString()
            };

            var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);

            if (result == null)
                return null;

            var idempotencia = new Idempotency(
                Guid.Parse(result.chave_idempotencia.ToString()),
                result.requisicao,
                result.resultado
            );

            return idempotencia;
        }
    }
}
