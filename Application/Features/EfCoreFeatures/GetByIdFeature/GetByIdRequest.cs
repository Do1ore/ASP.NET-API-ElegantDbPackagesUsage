namespace Application.Features.EfCoreFeatures.GetByIdFeature;

public record GetByIdRequest(Guid PhotoId ) : IRequest<Result<Photo>>;