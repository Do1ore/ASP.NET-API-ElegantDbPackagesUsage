using Infrastructure.Enums;

namespace Application.Features.EfCoreFeatures.DeleteFeature;

public record DeletePhotoRequest(Guid PhotoId, RepositoryType RepositoryType) : IRequest<Result<int>>;