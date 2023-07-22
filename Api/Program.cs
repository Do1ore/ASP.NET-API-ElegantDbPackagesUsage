using Api.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAndConfigureMediatR();

//Database configuration 
builder.Services.ConfigureEfCore(builder.Configuration);
builder.Services.ConfigureDapper(builder.Configuration);
builder.Services.ConfigureRedis(builder.Configuration);
//Repositories
builder.Services.AddCustomRepositories();
builder.Services.AddMediatRValidators();

//Health
builder.Services.AddConfiguredHealthChecks(builder.Configuration);

ConfigureLogger(builder.Configuration);
builder.Host.UseSerilog();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

//app.AddGlobalExceptionHandling();

app.RegisterEndpointDefinitions();

app.UseHttpsRedirection();

app.MapAndConfigureHealthChecks();

app.Run();

void ConfigureLogger(IConfiguration conf)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                      throw new Exception("Variable ASPNETCORE_ENVIRONMENT not found");

    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{environment}.json")
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureEls(conf, environment))
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureEls(IConfiguration config, string env)
{
    var connectionString = config.GetConnectionString("ElasticConnection") ??
                           throw new Exception("Connection for elasticsearch not found");

    return new ElasticsearchSinkOptions(new Uri(connectionString))
    {
        IndexFormat =
            $"{config["ApplicationName"]}-logs-" +
            $"{env.ToLower().Replace(".", "-")}-" +
            $"{DateTime.UtcNow:yyyy-MM)}",
        AutoRegisterTemplate = true,
        NumberOfShards = 2,
        NumberOfReplicas = 1,
    };
}