using FluentValidation;

namespace Application.Features.EfCoreFeatures.UpdateFeature;

public class UpdatePhotoRequestValidator : AbstractValidator<UpdatePhotoRequest>
{
    public UpdatePhotoRequestValidator()
    {
        RuleFor(a => a.Photo.PhotoName)
            .Must(l => l.All(char.IsLetter))
            .WithMessage("Photo name must contain only letters");

        RuleFor(a => a.Photo.FileExtension)
            .NotEmpty()
            .NotNull()
            .Must(a => a.Length < 10)
            .Must(a => a.Length > 2)
            .WithMessage("File extenstion length must be less than 10 and longer than 2 symbols");

        RuleFor(a => a.Photo.FileExtension)
            .Must(a => a.Any(letter => letter.Equals('.')))
            .WithMessage("File extenstion must contain a dot ");
    }
}