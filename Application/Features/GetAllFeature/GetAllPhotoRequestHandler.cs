using Application.Contracts;
using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Application.Features.EfCoreFeatures.GetAllFeature;

public class GetAllPhotosRequestHandler : IRequestHandler<GetAllPhotosRequest, Result<List<Photo>>>
{
    private readonly IRepositoryFactory _repositoryFactory;

    public GetAllPhotosRequestHandler(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }


    public async Task<Result<List<Photo>>> Handle(GetAllPhotosRequest request,
        CancellationToken cancellationToken)
    {
        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        var result = await repository.GetAllPhotos(cancellationToken);
        
        return result;
    }
}