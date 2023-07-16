using Infrastructure.Enums;

namespace Application.Features.EfCoreFeatures.UpdateFeature;

public record class UpdatePhotoRequest(Photo Photo, RepositoryType RepositoryType) : IRequest<Result<Photo>>;