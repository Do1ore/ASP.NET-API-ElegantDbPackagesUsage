using Domain.Entities;
using LanguageExt.Common;

namespace Application.Contracts;

public interface IDatabaseRepository<T> where T : class
{
    Task<Result<List<Photo>>> GetAllPhotos();
    Task<Result<Photo>> GetPhotoById(Guid id);
    Task<Result<Photo>> CreatePhoto(Photo photo);
    Task<Result<Photo>> UpdatePhoto(Photo photo);
    Task<Result<Photo>> DeletePhoto(Guid id);
}