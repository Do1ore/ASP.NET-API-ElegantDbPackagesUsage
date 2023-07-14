using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public record Photo
{
    public Photo()
    {
        Id = Guid.Empty;
        PhotoName = string.Empty;
        AbsolutePath = string.Empty;
        FileExtension = string.Empty;
        PhotographerId = Guid.Empty;
    }

    // public Photo(Guid id, string photoName, string absolutePath, string fileExtension, Photographer photographer)
    // {
    //     Id = id;
    //     PhotoName = photoName;
    //     AbsolutePath = absolutePath;
    //     FileExtension = fileExtension;
    //     Photographer = photographer;
    // }

    public Photo(Guid id, string photoName, string absolutePath, string fileExtension, Guid photographerId)
    {
        Id = id;
        PhotoName = photoName;
        AbsolutePath = absolutePath;
        FileExtension = fileExtension;
        PhotographerId = photographerId;
    }

    public Guid Id { get; init; }
    public string PhotoName { get; init; }
    public string AbsolutePath { get; init; }
    public string FileExtension { get; init; }
    [ForeignKey("Photographer")] public Guid? PhotographerId { get; init; }
    // public Photographer? Photographer { get; set; }
};