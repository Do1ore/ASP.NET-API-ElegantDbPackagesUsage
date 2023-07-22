using Domain.Entities;

namespace Api.DTOs;

public class PhotoDto
{
    public Guid Id { get; set; }
    public string PhotoName { get; init; } = string.Empty;
    public string AbsolutePath { get; init; } = string.Empty;
    public string FileExtension { get; init; } = string.Empty;

    public Guid? PhotographerId { get; set; } = Guid.Parse("bec9fa90-8ab6-4d87-91ce-22291c165d84");


    public static implicit operator Photo(PhotoDto photoDto)
    {
        return new Photo()
        {
            Id = photoDto.Id,
            PhotoName = photoDto.PhotoName,
            AbsolutePath = photoDto.AbsolutePath,
            FileExtension = photoDto.FileExtension,
            PhotographerId = (Guid)photoDto.PhotographerId!
        };
    }

    public static explicit operator PhotoDto(Photo photoDto)
    {
        return new PhotoDto()
        {
            Id = photoDto.Id,
            PhotoName = photoDto.PhotoName,
            AbsolutePath = photoDto.AbsolutePath,
            FileExtension = photoDto.FileExtension,
            PhotographerId = (Guid)photoDto.PhotographerId!
        };
    }
}