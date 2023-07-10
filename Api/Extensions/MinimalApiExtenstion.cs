using Api.Abstractions;
using Api.DTOs;

namespace Api.Extensions;

public static class MinimalApiExtenstion
{
    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
                        && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpoint in endpointDefinitions)
        {
            endpoint.RegisterEndpoints(app);
        }
    }

    public static void AddGlobalExceptionHandling(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                var errorModel = new ErrorModel(context.Response.StatusCode, "Internal server error occured");
                await context.Response.WriteAsJsonAsync(errorModel);
                throw;
            }
        });
    }
}