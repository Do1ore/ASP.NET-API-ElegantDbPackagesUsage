using Application.Contracts;
using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Application.Features.EfCoreFeatures.GetByIdFeature;

public class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, Result<Photo>>
{
    private readonly IRepositoryFactory _repositoryFactory;

    public GetByIdRequestHandler(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<Result<Photo>> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        var result = await repository.GetPhotoById(request.PhotoId, cancellationToken);
        return result;
    }
}