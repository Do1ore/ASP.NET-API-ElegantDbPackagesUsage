using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Data.EfCore;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfCoreRepository : IDatabaseRepository
{
    private readonly EfCorePhotosContext? _db;

    public EfCoreRepository(EfCorePhotosContext? db)
    {
        _db = db;
    }

    public async Task<Result<List<Photo>>> GetAllPhotos(CancellationToken cancellationToken)
    {
        var result = await _db.Photos!.ToListAsync(cancellationToken);
        return new Result<List<Photo>>(result);
    }

    public async Task<Result<Photo>> GetPhotoById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _db.Photos!.FindAsync(id);
        if (result.IsNull())
        {
            return new Result<Photo>(new KeyNotFoundException("Value with this key is not exists."));
        }

        return result!;
    }

    public async Task<Result<Photo>> CreatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        var result = await _db.Photos!
            .AddAsync(photo, cancellationToken);
        if (result.State != EntityState.Added)
        {
            return new Result<Photo>(new InvalidOperationException("Error while adding new value."));
        }

        await _db.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<Result<Photo>> UpdatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        var photoToUpdate = await _db.Photos!.FindAsync(photo.Id);

        if (photoToUpdate == null)
        {
            return new Result<Photo>(new ArgumentException("Value with this key is not exists."));
        }

        var result = _db.Photos.Update(photoToUpdate);

        if (result.State == EntityState.Modified)
        {
            return result.Entity;
        }
        else if (result.State == EntityState.Unchanged)
        {
            return result.Entity;
        }

        return new Result<Photo>(new ApplicationException("Unknown exception."));
    }

    public async Task<Result<int>> DeletePhoto(Guid id, CancellationToken cancellationToken)
    {
        var result = await _db.Photos!.Where(a => a.Id == id).ExecuteDeleteAsync(cancellationToken);
        return result;
    }

    public async Task<bool> IsPhotoExists(Guid id, CancellationToken cancellationToken)
    {
        var result = await _db.Photos!.AnyAsync(a => a.Id == id, cancellationToken);

        return result;
    }
}