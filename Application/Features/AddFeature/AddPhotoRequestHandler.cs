using Application.Contracts;
using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Application.Features.AddFeature;

public class AddPhotoRequestHandler : IRequestHandler<AddPhotoRequest, Result<Photo>>
{
    private readonly IValidator<AddPhotoRequest> _validator;
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IMediator _mediator;

    public AddPhotoRequestHandler(
        IRepositoryFactory repositoryFactory,
        IValidator<AddPhotoRequest> validator, IMediator mediator)
    {
        _validator = validator;
        _mediator = mediator;
        _repositoryFactory = repositoryFactory;
    }

    public async Task<Result<Photo>> Handle(AddPhotoRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return new Result<Photo>(new ValidationException(string.Join(", ", validationResult.Errors)));

        var repository = await _repositoryFactory.CreateRepository(request.RepositoryType);
        var result = await repository.CreatePhoto(request.Photo, cancellationToken);
        
        if (result.IsSuccess)
        {
            await _mediator.Publish(new AddPhotoNotification(request.Photo), cancellationToken);
        }


        return result;
    }
}