namespace Api.DTOs;

public class PhotoDto
{
    public Guid Id { get; init; }
    public string PhotoName { get; init; } = string.Empty;
    public string AbsolutePath { get; init; } = string.Empty;
    public string FileExtension { get; init; } = string.Empty;
}