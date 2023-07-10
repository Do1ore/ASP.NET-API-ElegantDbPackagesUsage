using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Features.EfCoreFeatures;

public class AddPhotoRequestHandler : IRequestHandler<AddPhotoRequest, Photo>
{
    private readonly IDatabaseRepository<EfCoreRepository> _repository;

    public AddPhotoRequestHandler(IDatabaseRepository<EfCoreRepository> repository)
    {
        _repository = repository;
    }

    public async Task<Photo> Handle(AddPhotoRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.CreatePhoto(request.Photo, cancellationToken);
        return result.Match(a => a, exception => throw exception);
    }
}