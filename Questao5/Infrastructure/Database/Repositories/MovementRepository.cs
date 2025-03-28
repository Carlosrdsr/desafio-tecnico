using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Extensions;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public MovementRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task AddAsync(Movement movement)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);

        var sql = @"INSERT INTO Movimento (IdMovimento, IdContaCorrente, DataMovimento, TipoMovimento, Valor) 
                    VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";

        var parameters = new
        {
            IdMovimento = movement.MovementId,
            IdContaCorrente = movement.CurrentAccountId.ToUpper(),
            DataMovimento = movement.MovementDate,
            TipoMovimento = movement.MovementType.ToCode(),
            Valor = movement.Value
        };

        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<decimal> GetSumByTypeAsync(string contaCorrenteId, EMovementType movementType)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);

        var query = @"SELECT COALESCE(SUM(Valor), 0)
                        FROM Movimento
                       WHERE IdContaCorrente = @ContaCorrenteId
                         AND TipoMovimento = @TipoMovimento";

        var parameters = new { ContaCorrenteId = contaCorrenteId, TipoMovimento = movementType.ToCode() };

        return await connection.ExecuteScalarAsync<decimal>(query, parameters);
    }
}
