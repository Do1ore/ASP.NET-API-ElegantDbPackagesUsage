using System.Reflection;
using Api.Abstractions;
using Api.DTOs;
using Application.Features.EfCoreFeatures;
using Application.Features.EfCoreFeatures.AddFeature;
using Application.Features.EfCoreFeatures.UpdateFeature;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Enums;

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
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                var errorModel = new ErrorModel(context.Response.StatusCode, "Internal server error occured");
                await context.Response.WriteAsJsonAsync(errorModel);
                throw;
            }
        });
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddPhotoRequest>, AddPhotoRequestValidator>();
        services.AddTransient<IValidator<UpdatePhotoRequest>, UpdatePhotoRequestValidator>();
    }


    public static RepositoryType ToRepositoryType(this string stringRepositoryType)
    {
        return stringRepositoryType.ToLower() switch
        {
            "efcore" => RepositoryType.EfCore,
            "adonet" => RepositoryType.AdoNet,
            _ => throw new ArgumentException($"Type {stringRepositoryType} is not exists for repository")
        };
    }
}