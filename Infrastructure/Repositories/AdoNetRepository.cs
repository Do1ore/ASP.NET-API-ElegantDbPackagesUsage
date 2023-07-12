using Domain.Entities;
using Infrastructure.Abstractions;
using LanguageExt.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class AdoNetRepository : IDatabaseRepository
{
    private readonly string _connectionString;

    public AdoNetRepository(IConfiguration? configuration)
    {
        _connectionString = configuration!.GetConnectionString("PostgreSQLConnection") ??
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

    public async Task<Result<Photo>> GetPhotoById(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var commandText = """
        SELECT "Id", "PhotoName", "AbsolutePath", "FileExtension"
        FROM public."Photos" p
        WHERE p."Id" = @id
        """;


        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@id", id);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        Photo result = new();
        while (await reader.ReadAsync(cancellationToken))
        {
            var photoId = reader.GetGuid(0);
            var photoName = reader.GetString(1);
            var absolutePath = reader.GetString(2);
            var fileExtenstion = reader.GetString(3);
            result = new Photo(photoId, photoName, absolutePath, fileExtenstion);
        }

        return result;
    }

    public async Task<Result<Photo>> CreatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var commandText = $@"
        INSERT INTO public.""Photos"" (""Id"", ""PhotoName"", ""AbsolutePath"", ""FileExtension"")
        VALUES (@Id, @PhotoName, @AbsolutePath, @FileExtension)
        RETURNING ""Id"", ""PhotoName"", ""AbsolutePath"", ""FileExtension""";

        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@Id", photo.Id);
        command.Parameters.AddWithValue("@PhotoName", photo.PhotoName);
        command.Parameters.AddWithValue("@AbsolutePath", photo.AbsolutePath);
        command.Parameters.AddWithValue("@FileExtension", photo.FileExtension);

        var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var id = reader.GetGuid(0);
            var photoName = reader.GetString(1);
            var absolutePath = reader.GetString(2);
            var fileExtension = reader.GetString(3);
            return new Photo(id, photoName, absolutePath, fileExtension);
        }

        return new Result<Photo>(new ArgumentException("Failed to create photo."));
    }

    public async Task<Result<Photo>> UpdatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var commandText = """
        UPDATE public."Photos"
        SET "PhotoName" = @PhotoName, "AbsolutePath" = @AbsolutePath, "FileExtension" = @FileExtension
        WHERE "Id" = @Id
        RETURNING "Id", "PhotoName", "AbsolutePath", "FileExtension"
        """;

        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@Id", photo.Id);
        command.Parameters.AddWithValue("@PhotoName", photo.PhotoName);
        command.Parameters.AddWithValue("@AbsolutePath", photo.AbsolutePath);
        command.Parameters.AddWithValue("@FileExtension", photo.FileExtension);

        var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var id = reader.GetGuid(0);
            var photoName = reader.GetString(1);
            var absolutePath = reader.GetString(2);
            var fileExtension = reader.GetString(3);
            return new Photo(id, photoName, absolutePath, fileExtension);
        }

        return new Result<Photo>(new ArgumentException("Failed to update photo."));
    }

    public async Task<Result<int>> DeletePhoto(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var commandText = """
        DELETE FROM public."Photos"
        WHERE "Id" = @Id;
        """;

        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@Id", id);

        var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (rowsAffected > 0)
        {
            return rowsAffected;
        }

        return new Result<int>(new ArgumentException("Failed to delete entity"));
    }

    public async Task<bool> IsPhotoExists(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var commandText = """
        SELECT COUNT(*)
        FROM public."Photos"
        WHERE "Id" = @Id
        """;

        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@Id", id);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        var count = Convert.ToInt32(result);
        return count > 0;
    }
}