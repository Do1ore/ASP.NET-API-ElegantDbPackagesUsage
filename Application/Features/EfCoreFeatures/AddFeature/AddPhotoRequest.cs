using LanguageExt.Common;

namespace Application.Features.EfCoreFeatures.AddFeature;

public record class AddPhotoRequest(Photo Photo) : IRequest<Result<Photo>>;