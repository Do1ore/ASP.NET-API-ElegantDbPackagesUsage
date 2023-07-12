using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Application.Features.EfCoreFeatures.GetAllFeature;

public class GetAllPhotosRequestHandler : IRequestHandler<GetAllPhotosRequest, Result<List<Photo>>>
{
    private readonly IDatabaseRepository<EfCoreRepository> _repository;


    public GetAllPhotosRequestHandler(IDatabaseRepository<EfCoreRepository> repository)
    {
        _repository = repository;
    }


    public async Task<Result<List<Photo>>> Handle(GetAllPhotosRequest photosRequest,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllPhotos(cancellationToken);


        return result;
    }
}