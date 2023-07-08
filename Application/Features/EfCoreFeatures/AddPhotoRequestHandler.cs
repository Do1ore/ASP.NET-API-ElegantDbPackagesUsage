using Domain.Entities;
using MediatR;

namespace Application.Features.EfCoreFeatures;

public class AddPhotoRequestHandler : IRequestHandler<AddPhotoRequest, Photo>
{
    public Task<Photo> Handle(AddPhotoRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Photo());
    }
}