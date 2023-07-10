using Infrastructure.Data.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQLConnection") ??
                               throw new InvalidOperationException("Connection string not found");

        services.AddDbContext<EfCorePhotosContext>(options => options
            .UseNpgsql(connectionString));
    }
}