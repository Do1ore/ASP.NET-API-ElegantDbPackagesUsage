using Infrastructure.Data.EfCore;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection") ??
                               throw new InvalidOperationException("Connection string not found");

        services.AddDbContext<EfCorePhotosContext>(options => options
            .UseNpgsql(connectionString));
    }
}