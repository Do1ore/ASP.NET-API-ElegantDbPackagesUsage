using Infrastructure.Enums;

namespace Application.Features.DeleteFeature;

public record DeletePhotoRequest(Guid PhotoId, RepositoryType RepositoryType) : IRequest<Result<int>>;