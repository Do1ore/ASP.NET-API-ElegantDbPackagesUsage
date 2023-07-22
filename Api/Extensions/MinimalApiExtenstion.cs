using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Api.Abstractions;
using Application.Features.AddFeature;
using Application.Features.UpdateFeature;
using Domain.Common;
using FluentValidation;
using HealthChecks.UI.Client;
using Infrastructure.Enums;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Sinks.Elasticsearch;

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

    public static void ConfigureLogging(this IHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            var connection = context.Configuration.GetConnectionString("ElasticConnection") ??
                             throw new Exception("Connection string for elastic search not found");

            configuration.Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(connection))
                    {
                        IndexFormat =
                            $"{context.Configuration["ApplicationName"]}-logs-" +
                            $"{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-" +
                            $"{DateTime.UtcNow:yyyy-MM)}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1,
                    })
                .Enrich.WithProperty("Environment",
                    context.HostingEnvironment.EnvironmentName ??
                    throw new InvalidOperationException("Environment name not found"))
                .ReadFrom.Configuration(context.Configuration);
        });
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

    public static IResult ToOkResult<TResult, TContract>(
        this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match<IResult>(obj =>
        {
            var response = mapper(obj);
            Log.Information("Success operation with {@Type}", typeof(TResult));
            return TypedResults.Ok(response);
        }, exception =>
        {
            Log.Error("Cannot create new entity typeof {@Entity}. Exception details: {@Exception}",
                typeof(TResult), exception);
            return TypedResults.BadRequest(new ErrorModel(StatusCodes.Status400BadRequest, exception.Message));
        });
    }

    public static void AddConfiguredHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("PostgreSQLConnection") ??
                       throw new Exception("Connection for npgsql not found"))
            .AddRedis(configuration.GetConnectionString("RedisConnection") ??
                      throw new Exception("Connection for redis not found"))
            .AddElasticsearch(configuration.GetConnectionString("ElasticConnection") ??
                              throw new Exception("Connection for elasticsearch not found"));
    }

    public static void MapAndConfigureHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/_health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}