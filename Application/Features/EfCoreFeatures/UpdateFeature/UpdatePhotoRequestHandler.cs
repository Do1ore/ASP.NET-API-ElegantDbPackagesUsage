using FluentValidation;
using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Application.Features.EfCoreFeatures.UpdateFeature;

public class UpdatePhotoRequestHandler : IRequestHandler<UpdatePhotoRequest, Result<Photo>>
{
    private readonly IDatabaseRepository<EfCoreRepository> _repository;
    private readonly IValidator<UpdatePhotoRequest> _validator;

    public UpdatePhotoRequestHandler(IDatabaseRepository<EfCoreRepository> repository,
        IValidator<UpdatePhotoRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }


    public async Task<Result<Photo>> Handle(UpdatePhotoRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new Result<Photo>(new ArgumentException(string.Join(",", validationResult.Errors)));

        if (!await _repository.IsPhotoExists(request.Photo.Id, cancellationToken))
            return new Result<Photo>(new ArgumentException("Value with this key is not exists"));

        var result = await _repository.UpdatePhoto(request.Photo, cancellationToken);

        return result;
    }
}