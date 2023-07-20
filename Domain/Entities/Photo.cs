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

    public Photo(Guid id, string photoName, string absolutePath, string fileExtension, Photographer photographer)
    {
        Id = id;
        PhotoName = photoName;
        AbsolutePath = absolutePath;
        FileExtension = fileExtension;
        Photographer = photographer;
    }

    public Photo(Guid id, string photoName, string absolutePath, string fileExtension, Guid photographerId)
    {
        Id = id;
        PhotoName = photoName;
        AbsolutePath = absolutePath;
        FileExtension = fileExtension;
        PhotographerId = photographerId;
    }

    [Column("Id")] public Guid Id { get; init; }
    [Column("PhotoName")] public string PhotoName { get; init; }
    [Column("AbsolutePath")] public string AbsolutePath { get; init; }
    [Column("FileExtension")] public string FileExtension { get; init; }

    [Column("PhotographerId")]
    [ForeignKey("Photographer")]
    public Guid PhotographerId { get; init; }

    public Photographer? Photographer { get; set; }
};