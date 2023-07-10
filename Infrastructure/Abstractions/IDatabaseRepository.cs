using Domain.Entities;
using LanguageExt.Common;

namespace Infrastructure.Abstractions;

public interface IDatabaseRepository<T> where T : class
{
    Task<Result<List<Photo>>> GetAllPhotos(CancellationToken cancellationToken);
    Task<Result<Photo>> GetPhotoById(Guid id, CancellationToken cancellationToken);
    Task<Result<Photo>> CreatePhoto(Photo photo, CancellationToken cancellationToken);
    Task<Result<Photo>> UpdatePhoto(Photo photo, CancellationToken cancellationToken);
    Task<Result<int>> DeletePhoto(Guid id, CancellationToken cancellationToken);
}