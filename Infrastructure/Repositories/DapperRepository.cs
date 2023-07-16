using System.Data;
using Dapper;
using Domain.Entities;
using Infrastructure.Abstractions;
using LanguageExt.Common;

namespace Infrastructure.Repositories;

public class DapperRepository : IDatabaseRepository
{
    private readonly IDbConnection _dbConnection;

    public DapperRepository(IDbContext db)
    {
        _dbConnection = db.GetConnection();
    }

    public async Task<Result<List<Photo>>> GetAllPhotos(CancellationToken cancellationToken)
    {
        var commandText = """
        SELECT "Id", "PhotoName", "AbsolutePath", "FileExtension", "PhotographerId"
        FROM public."Photos";
        """;
        var result = await _dbConnection.QueryAsync<Photo>(commandText);
        return result.ToList();
    }

    public Task<Result<Photo>> GetPhotoById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Photo>> CreatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Photo>> UpdatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<int>> DeletePhoto(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsPhotoExists(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}