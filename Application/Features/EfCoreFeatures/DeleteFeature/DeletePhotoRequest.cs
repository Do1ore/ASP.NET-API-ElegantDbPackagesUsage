namespace Application.Features.EfCoreFeatures.DeleteFeature;

public record DeletePhotoRequest(Guid PhotoId) : IRequest<Result<int>>;