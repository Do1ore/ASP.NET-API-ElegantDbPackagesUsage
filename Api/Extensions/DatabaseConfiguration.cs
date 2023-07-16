using Infrastructure.Abstractions;
using Infrastructure.Data.Dapper;
using Infrastructure.Data.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQLConnection") ??
                               throw new InvalidOperationException("Connection string not found");

        services.AddDbContext<EfCorePhotosContext>(options => options
            .UseNpgsql(connectionString));
        return services;
    }

    public static IServiceCollection ConfigureDapper(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQLConnection") ??
                               throw new InvalidOperationException("Connection string not found");

        services.AddSingleton<IDbContext>(new PostgreDbContext(connectionString));
        
        return services;
    }
}