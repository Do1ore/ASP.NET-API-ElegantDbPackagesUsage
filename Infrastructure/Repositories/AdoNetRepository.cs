using Domain.Entities;
using Infrastructure.Abstractions;
using LanguageExt.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query;

namespace Infrastructure.Repositories;

public class AdoNetRepository : IDatabaseRepository<AdoNetRepository>
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public AdoNetRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("PostgreSQLConnection") ??
                            throw new InvalidOperationException("Connection string not found");
    }

    public async Task<Result<List<Photo>>> GetAllPhotos(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var commandText = """
        SELECT "Id", "PhotoName", "AbsolutePath", "FileExtension"
        FROM public."Photos";
        """;

        var result = new List<Photo>();
        await using var command = new NpgsqlCommand(commandText, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var id = reader.GetGuid(0);
            var photoName = reader.GetString(1);
            var absolutePath = reader.GetString(2);
            var fileExtenstion = reader.GetString(3);
            result.Add(new Photo(id, photoName, absolutePath, fileExtenstion));
        }

        return result;
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