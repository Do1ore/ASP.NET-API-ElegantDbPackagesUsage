using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public record Photographer(Guid Id, string Name, DateTime WasBorn)
{
    [Key]
    public Guid Id { get; set; } = Id;

    public string Name { get; set; } = Name;
    public DateTime WasBorn { get; set; } = WasBorn;

    [NotMapped]
    public ICollection<Photo>? Photos { get; set; }
    
    public Photographer() : this(Guid.Empty, string.Empty, DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)))
    {
    }
}