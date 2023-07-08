using Domain.Entities;
using MediatR;

namespace Application.Features.EfCoreFeatures;

public record class AddPhotoRequest(Photo Photo) : IRequest<Photo>
{
}