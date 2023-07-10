namespace Application.Features.EfCoreFeatures.GetAllFeature;

public record class GetAllPhotosRequest : IRequest<Result<List<Photo>>>;