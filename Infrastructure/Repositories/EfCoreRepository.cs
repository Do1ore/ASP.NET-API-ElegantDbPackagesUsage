using Application.Contracts;
using Domain.Entities;
using LanguageExt.Common;

namespace Infrastructure.Repositories;

public class EfCoreRepository : IDatabaseRepository<EfCoreRepository>
{
    public Task<Result<List<Photo>>> GetAllPhotos()
    {
        throw new NotImplementedException();
    }

    public Task<Result<Photo>> GetPhotoById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Photo>> CreatePhoto(Photo photo)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Photo>> UpdatePhoto(Photo photo)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Photo>> DeletePhoto(Guid id)
    {
        throw new NotImplementedException();
    }
}