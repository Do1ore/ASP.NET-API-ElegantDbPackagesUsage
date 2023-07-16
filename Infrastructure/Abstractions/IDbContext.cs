using System.Data;

namespace Infrastructure.Abstractions;

public interface IDbContext
{
    IDbConnection GetConnection();
}