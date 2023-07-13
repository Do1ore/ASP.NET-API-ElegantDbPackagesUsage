using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EfCore;

public class EfCorePhotosContext : DbContext
{
    public EfCorePhotosContext()
    {
    }

    public EfCorePhotosContext(DbContextOptions<EfCorePhotosContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Server=localhost;Port=5432;Database=MultiOperation;Username=postgres;Password=postgre;Integrated Security=true;Pooling=true;");
        }
    }

    public DbSet<Photo>? Photos { get; set; }

    public DbSet<Photographer>? Photographers { get; set; }
}