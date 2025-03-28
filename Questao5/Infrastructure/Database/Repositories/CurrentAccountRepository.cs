using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;

public class CurrentAccountRepository : ICurrentAccountRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public CurrentAccountRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public async Task<CurrentAccount> GetAccountByIdAsync(string id)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);

        var sql = @"SELECT 
                        IdContaCorrente, Numero, Nome, Ativo 
                      FROM ContaCorrente
                     WHERE IdContaCorrente = @Id";

        var parameters = new
        {
            Id = id.ToUpper(),
        };

        var contaCorrente = await connection.QueryFirstOrDefaultAsync<CurrentAccount>(sql, parameters);

        return contaCorrente;
    }
}
