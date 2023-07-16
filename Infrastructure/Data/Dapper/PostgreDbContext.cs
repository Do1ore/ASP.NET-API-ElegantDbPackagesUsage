using System.Data;
using Infrastructure.Abstractions;
using Npgsql;

namespace Infrastructure.Data.Dapper;

public sealed class PostgreDbContext : IDbContext
{
    private readonly string _connectionString;

    public PostgreDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}