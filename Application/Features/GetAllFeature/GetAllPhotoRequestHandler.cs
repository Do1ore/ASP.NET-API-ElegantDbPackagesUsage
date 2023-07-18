using Application.Contracts;
using Infrastructure.Abstractions;

namespace Application.Features.GetAllFeature;

public class GetAllPhotosRequestHandler : IRequestHandler<GetAllPhotosRequest, Result<List<Photo>>>
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IRedisService _redisService;

    public GetAllPhotosRequestHandler(IRepositoryFactory repositoryFactory, 
        IRedisService redisService)
    {
        _repositoryFactory = repositoryFactory;
        _redisService = redisService;
    }


    public async Task<Result<List<Photo>>> Handle(GetAllPhotosRequest request,
        CancellationToken cancellationToken)
    {
               
        
        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        var result = await repository.GetAllPhotos(cancellationToken);
        
        return result;
    }
}