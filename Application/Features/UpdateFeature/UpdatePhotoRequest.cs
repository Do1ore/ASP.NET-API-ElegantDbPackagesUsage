using Infrastructure.Enums;

namespace Application.Features.UpdateFeature;

public record class UpdatePhotoRequest(Photo Photo, RepositoryType RepositoryType) : IRequest<Result<Photo>>;