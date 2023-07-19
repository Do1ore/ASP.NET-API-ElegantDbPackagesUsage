using Infrastructure.Enums;

namespace Application.Features.AddFeature;

public record AddPhotoRequest(Photo Photo, RepositoryType RepositoryType) : IRequest<Result<Photo>>;