using Application.Contracts;
using Application.Factories;

namespace Api.Extensions;

public static class RepositoriesConfiguration
{
    public static IServiceCollection AddCustomRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryFactory), typeof(RepositoryFactory));
        return services;
    }
}