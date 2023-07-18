using System.Reflection;
using Api.Abstractions;
using Application.Features.AddFeature;
using Application.Features.UpdateFeature;
using Domain.Common;
using FluentValidation;
using Infrastructure.Enums;
using MediatR;

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

    public static void AddAndConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(
                Assembly.GetAssembly(typeof(AddPhotoRequestHandler)) ??
                throw new InvalidOperationException()));
    }

    public static void AddGlobalExceptionHandling(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                var errorModel = new ErrorModel(context.Response.StatusCode, ex.Message);
                await context.Response.WriteAsJsonAsync(errorModel);
            }
        });
    }

    public static void AddMediatRValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddPhotoRequest>, AddPhotoRequestValidator>();
        services.AddTransient<IValidator<UpdatePhotoRequest>, UpdatePhotoRequestValidator>();
    }

    public static void AddMediatRNotifications(this IServiceCollection services)
    {
   
    }


    public static RepositoryType ToRepositoryType(this string stringRepositoryType)
    {
        return stringRepositoryType.ToLower() switch
        {
            "efcore" => RepositoryType.EfCore,
            "adonet" => RepositoryType.AdoNet,
            "dapper" => RepositoryType.Dapper,
            _ => throw new ArgumentException($"Type [{stringRepositoryType}] is unknown.")
        };
    }
}