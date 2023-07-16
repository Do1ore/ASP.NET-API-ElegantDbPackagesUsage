using System.Data;
using System.Text.RegularExpressions;
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
        const string commandText = """
        SELECT "Id", "PhotoName", "AbsolutePath", "FileExtension", "PhotographerId"
        FROM public."Photos";
        """;
        var result = await _dbConnection.QueryAsync<Photo>(commandText, cancellationToken);
        return result.ToList();
    }

    public async Task<Result<Photo>> GetPhotoById(Guid id, CancellationToken cancellationToken)
    {
        const string commandText = """
        SELECT "Id", "PhotoName", "AbsolutePath", "FileExtension", "PhotographerId"
        FROM public."Photos" p
        WHERE p."Id" = @id;
        """;
        var commandParameters = new { id = id };

        var result = await _dbConnection.QueryFirstAsync<Photo>(commandText, commandParameters);
        return result;
    }

    public async Task<Result<Photo>> CreatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        const string commandText = """
        INSERT INTO public."Photos" ("Id", "PhotoName", "AbsolutePath", "FileExtension", "PhotographerId")
        VALUES (@Id, @PhotoName, @AbsolutePath, @FileExtension, @PhotographerId)
        RETURNING "Id", "PhotoName", "AbsolutePath", "FileExtension", "PhotographerId"
        """;
        var result = await _dbConnection.ExecuteAsync(commandText, photo);
        if (result < 1)
        {
            return new Result<Photo>(new ArgumentException("Entity with given parameters cannot be created"));
        }

        return photo;
    }

    public async Task<Result<Photo>> UpdatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        const string commandText = """
        UPDATE public."Photos"
        SET "PhotoName" = @PhotoName, "AbsolutePath" = @AbsolutePath, 
        "FileExtension" = @FileExtension, "PhotographerId" = @PhotographerId
        WHERE "Id" = @Id
        RETURNING "Id", "PhotoName", "AbsolutePath", "FileExtension", "PhotographerId"
        """;
        var result = await _dbConnection.ExecuteAsync(commandText, photo);
        if (result < 1)
        {
            return new Result<Photo>(new ArgumentException("Entity with given parameters cannot be updated"));
        }

        return photo;
    }

    public async Task<Result<int>> DeletePhoto(Guid id, CancellationToken cancellationToken)
    {
        const string commandText = """
        DELETE FROM public."Photos"
        WHERE "Id" = @Id;
        """;
        var commandParameters = new { id = id };
        var result = await _dbConnection.ExecuteAsync(commandText, commandParameters);
        if (result < 1)
        {
            return new Result<int>(new ArgumentException("Entity with given parameters cannot be updated"));
        }

        return result;
    }

    public async Task<bool> IsPhotoExists(Guid id, CancellationToken cancellationToken)
    {
        const string commandText = """
        SELECT COUNT(*)
        FROM public."Photos" p 
        WHERE p."Id" = @Id
        """;

        var commandParameters = new { id = id };
        var result = await _dbConnection.QueryAsync<Photo>(commandText, commandParameters);
        if (result.Count() < 0)
        {
            return false;
        }

        return true;
    }
}