using Application.Contracts;
using Infrastructure.Abstractions;

namespace Application.Features.GetByIdFeature;

public class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, Result<Photo>>
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IRedisService _redisService;
    private readonly IMediator _mediator;

    public GetByIdRequestHandler(IRepositoryFactory repositoryFactory,
        IMediator mediator,
        IRedisService redisService)
    {
        _repositoryFactory = repositoryFactory;
        _mediator = mediator;
        _redisService = redisService;
    }

    public async Task<Result<Photo>> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await _redisService.GetById<Photo>(request.PhotoId);
        if (entity.IsSuccess)
        {
            return entity;
        }

        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        var result = await repository.GetPhotoById(request.PhotoId, cancellationToken);


        await result.Match<Task<Result<Photo>>>(async value =>
        {
            await _mediator.Publish(new GetByIdNotification(value), cancellationToken);
            return result;
        }, ex => Task.FromResult(result));

        return result;
    }
}