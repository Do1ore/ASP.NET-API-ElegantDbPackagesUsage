using Application.Contracts;
using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Application.Features.EfCoreFeatures.DeleteFeature;

public class DeletePhotoRequestHandler : IRequestHandler<DeletePhotoRequest, Result<int>>
{

    private readonly IRepositoryFactory _repositoryFactory;

    public DeletePhotoRequestHandler(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<Result<int>> Handle(DeletePhotoRequest request, CancellationToken cancellationToken)
    {
        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        if (!await repository.IsPhotoExists(request.PhotoId, cancellationToken))
            return new Result<int>(new ArgumentException("Value with this key is not exists"));
        
        var result = await repository.DeletePhoto(request.PhotoId, cancellationToken);
        return result;
    }
}