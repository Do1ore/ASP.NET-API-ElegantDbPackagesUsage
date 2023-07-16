using Infrastructure.Enums;

namespace Application.Features.EfCoreFeatures.GetByIdFeature;

public record GetByIdRequest(Guid PhotoId, RepositoryType RepositoryType) : IRequest<Result<Photo>>;