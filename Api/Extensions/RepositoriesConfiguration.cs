using Infrastructure.Abstractions;
using Infrastructure.Repositories;

namespace Api.Extensions;

public static class RepositoriesConfiguration
{
    public static void AddEfCoreCustomRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IDatabaseRepository<EfCoreRepository>), typeof(EfCoreRepository));
    }
}