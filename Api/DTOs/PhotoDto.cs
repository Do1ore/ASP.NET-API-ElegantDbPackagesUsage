using Domain.Entities;

namespace Api.DTOs;

public class PhotoDto
{
    public Guid Id { get; init; }
    public string PhotoName { get; init; } = string.Empty;
    public string AbsolutePath { get; init; } = string.Empty;
    public string FileExtension { get; init; } = string.Empty;


    public static implicit operator Photo(PhotoDto photoDto)
    {
        return new Photo()
        {
            Id = photoDto.Id,
            PhotoName = photoDto.PhotoName,
            AbsolutePath = photoDto.AbsolutePath,
            FileExtension = photoDto.FileExtension
        };
    }
}