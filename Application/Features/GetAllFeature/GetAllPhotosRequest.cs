using Infrastructure.Enums;

namespace Application.Features.GetAllFeature;

public record GetAllPhotosRequest(RepositoryType RepositoryType) : IRequest<Result<List<Photo>>>;