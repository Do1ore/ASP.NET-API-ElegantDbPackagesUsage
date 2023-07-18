using Infrastructure.Abstractions;
using Infrastructure.Data.Dapper;
using Infrastructure.Data.EfCore;
using Infrastructure.Data.Redis;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DatabasePackagesConfiguration
{
    public static IServiceCollection ConfigureEfCore(this IServiceCollection services, IConfiguration configuration)
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

    public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRedisContext, RedisContext>();
        services.AddScoped<IRedisService, RedisService>();
        
        return services;
    }
}