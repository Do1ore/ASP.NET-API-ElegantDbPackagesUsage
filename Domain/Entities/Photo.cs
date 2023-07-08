using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public record Photo
{
    public Photo()
    {
        Id = Guid.Empty;
        PhotoName = string.Empty;
        AbsolutePath = string.Empty;
        FileExtension = string.Empty;
    }

    public Photo(Guid Id, string PhotoName, string AbsolutePath, string FileExtension)
    {
        this.Id = Id;
        this.PhotoName = PhotoName;
        this.AbsolutePath = AbsolutePath;
        this.FileExtension = FileExtension;
    }

    public Guid Id { get; init; }
    public string PhotoName { get; init; }
    public string AbsolutePath { get; init; }
    public string FileExtension { get; init; }

    public void Deconstruct(out Guid Id, out string PhotoName, out string AbsolutePath, out string FileExtension)
    {
        Id = this.Id;
        PhotoName = this.PhotoName;
        AbsolutePath = this.AbsolutePath;
        FileExtension = this.FileExtension;
    }
};