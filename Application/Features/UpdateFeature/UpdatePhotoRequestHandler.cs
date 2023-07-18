using Application.Contracts;
using FluentValidation;

namespace Application.Features.UpdateFeature;

public class UpdatePhotoRequestHandler : IRequestHandler<UpdatePhotoRequest, Result<Photo>>
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IValidator<UpdatePhotoRequest> _validator;
    private readonly IMediator _mediator;

    public UpdatePhotoRequestHandler(IValidator<UpdatePhotoRequest> validator,
        IRepositoryFactory repositoryFactory,
        IMediator mediator)
    {
        _validator = validator;
        _repositoryFactory = repositoryFactory;
        _mediator = mediator;
    }


    public async Task<Result<Photo>> Handle(UpdatePhotoRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return new Result<Photo>(new ArgumentException(string.Join(",", validationResult.Errors)));

        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        if (!await repository.IsPhotoExists(request.Photo.Id, cancellationToken))
            return new Result<Photo>(new ArgumentException("Value with this key is not exists"));

        var result = await repository.UpdatePhoto(request.Photo, cancellationToken);

        if (result.IsSuccess)
        {
            await _mediator.Publish(new UpdatePhotoNotification(request.Photo));
        }

        return result;
    }
}