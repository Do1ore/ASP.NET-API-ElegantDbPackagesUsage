using Infrastructure.Enums;

namespace Application.Features.GetByIdFeature;

public record GetByIdRequest(Guid PhotoId, RepositoryType RepositoryType) : IRequest<Result<Photo>>;