using Infrastructure.Enums;

namespace Application.Features.EfCoreFeatures.AddFeature;

public record class AddPhotoRequest(Photo Photo, RepositoryType RepositoryType) : IRequest<Result<Photo>>;