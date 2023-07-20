using Application.Contracts;

namespace Application.Features.DeleteFeature;

public class DeletePhotoRequestHandler : IRequestHandler<DeletePhotoRequest, Result<int>>
{

    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IMediator _mediator;

    public DeletePhotoRequestHandler(IRepositoryFactory repositoryFactory,
        IMediator mediator)
    {
        _repositoryFactory = repositoryFactory;
        _mediator = mediator;
    }

    public async Task<Result<int>> Handle(DeletePhotoRequest request, CancellationToken cancellationToken)
    { 
        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        if (!await repository.IsPhotoExists(request.PhotoId, cancellationToken))
            return new Result<int>(new ArgumentException("Value with this key is not exists"));

        var result = await repository.DeletePhoto(request.PhotoId, cancellationToken);
        if (result.IsSuccess)
        {
            await _mediator.Publish(new DeletePhotoNotification(request.PhotoId));
        }
        return result;
    }
}