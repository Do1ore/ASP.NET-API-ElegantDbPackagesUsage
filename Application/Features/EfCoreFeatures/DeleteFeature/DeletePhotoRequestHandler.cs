using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Application.Features.EfCoreFeatures.DeleteFeature;

public class DeletePhotoRequestHandler : IRequestHandler<DeletePhotoRequest, Result<int>>
{
    private readonly IDatabaseRepository<EfCoreRepository> _repository;

    public DeletePhotoRequestHandler(IDatabaseRepository<EfCoreRepository> repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(DeletePhotoRequest request, CancellationToken cancellationToken)
    {
        if (!await _repository.IsPhotoExists(request.PhotoId, cancellationToken))
            return new Result<int>(new ArgumentException("Value with this key is not exists"));
        
        var result = await _repository.DeletePhoto(request.PhotoId, cancellationToken);
        return result;
    }
}