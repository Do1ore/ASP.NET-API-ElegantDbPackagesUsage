using Domain.Entities;
using LanguageExt;
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
                "User ID=postgres;Password=postgre;Server=db_server;Port=5432;Database=MultiOperation;Pooling=true");
        }
    }

    public DbSet<Photo>? Photos { get; set; }

    public DbSet<Photographer>? Photographers { get; set; }
}