using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EfCore;

public class EfCorePhotosContext : DbContext
{
    public EfCorePhotosContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Photo>? Photos { get; set; }
}