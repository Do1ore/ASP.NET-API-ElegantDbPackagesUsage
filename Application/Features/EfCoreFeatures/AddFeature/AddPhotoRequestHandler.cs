using FluentValidation;
using Infrastructure.Abstractions;
using Infrastructure.Repositories;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Application.Features.EfCoreFeatures.AddFeature;

public class AddPhotoRequestHandler : IRequestHandler<AddPhotoRequest, Result<Photo>>
{
    private readonly IDatabaseRepository<EfCoreRepository> _repository;
    private readonly IValidator<AddPhotoRequest> _validator;

    public AddPhotoRequestHandler(IDatabaseRepository<EfCoreRepository> repository,
        IValidator<AddPhotoRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Photo>> Handle(AddPhotoRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return new Result<Photo>(new ValidationException(string.Join(", ", validationResult.Errors)));

        var result = await _repository.CreatePhoto(request.Photo, cancellationToken);
        return result;
    }
}