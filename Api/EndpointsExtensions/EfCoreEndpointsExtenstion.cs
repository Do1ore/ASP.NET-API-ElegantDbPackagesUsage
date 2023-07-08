using Api.Endpoints;

namespace Api.EndpointsExtensions;

public static class EfCoreEndpointsExtenstion
{
    public static void AddEfCoreEndpoints(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
    }
}