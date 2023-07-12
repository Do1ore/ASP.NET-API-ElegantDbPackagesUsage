using Infrastructure.Enums;

namespace Application.Features.EfCoreFeatures.GetAllFeature;

public record GetAllPhotosRequest(RepositoryType RepositoryType) : IRequest<Result<List<Photo>>>;