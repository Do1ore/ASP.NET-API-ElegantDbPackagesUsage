using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Data.EfCore;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfCoreRepository : IDatabaseRepository<EfCoreRepository>
{
    private readonly EfCorePhotosContext _db;

    public EfCoreRepository(EfCorePhotosContext db)
    {
        _db = db;
    }

    public async Task<Result<List<Photo>>> GetAllPhotos(CancellationToken cancellationToken)
    {
        var result = await _db.Photos.ToListAsync(cancellationToken);
        return new Result<List<Photo>>(result);
    }

    public async Task<Result<Photo>> GetPhotoById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _db.Photos.FindAsync(id);
        if (result.IsNull())
        {
            return new Result<Photo>(new KeyNotFoundException("Value with this Id is not exists."));
        }

        return result!;
    }

    public async Task<Result<Photo>> CreatePhoto(Photo photo, CancellationToken cancellationToken)
    {
        var result = await _db.Photos
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
        await _db.Photos
            .Where(a => a.Id == photo.Id)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(a => a, photo),
                cancellationToken);

        return photo;
    }

    public async Task<Result<int>> DeletePhoto(Guid id, CancellationToken cancellationToken)
    {
        var result = await _db.Photos.Where(a => a.Id == id).ExecuteDeleteAsync(cancellationToken);
        return result;
    }
}