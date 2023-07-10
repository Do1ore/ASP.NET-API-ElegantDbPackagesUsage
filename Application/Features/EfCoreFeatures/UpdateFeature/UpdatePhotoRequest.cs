namespace Application.Features.EfCoreFeatures.UpdateFeature;

public record class UpdatePhotoRequest(Photo Photo) : IRequest<Result<Photo>>;