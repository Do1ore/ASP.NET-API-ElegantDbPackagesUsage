using LanguageExt.Common;

namespace Application.Features.EfCoreFeatures;

public record class AddPhotoRequest(Photo Photo) : IRequest<Result<Photo>>;