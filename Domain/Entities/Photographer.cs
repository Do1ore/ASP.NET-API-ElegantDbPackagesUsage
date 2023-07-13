using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public record Photographer
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public DateTime WasBorn { get; set; }
    [NotMapped]
    public ICollection<Photo>? Photos { get; set; }
    
    public Photographer()
    {
        Id= Guid.Empty;
        Name = String.Empty;
        WasBorn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(3));
    }

    public Photographer(Guid id, string name, DateTime wasBorn)
    {
        Id = id;
        Name = name;
        WasBorn = wasBorn;
    }
}