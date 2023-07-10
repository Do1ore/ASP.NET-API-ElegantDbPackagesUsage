using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Application.Features.EfCoreFeatures.GetByIdFeature;

public class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, Result<Photo>>
{
    private readonly IDatabaseRepository<EfCoreRepository> _repository;

    public GetByIdRequestHandler(IDatabaseRepository<EfCoreRepository> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Photo>> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPhotoById(request.PhotoId, cancellationToken);
        return result;
    }
}