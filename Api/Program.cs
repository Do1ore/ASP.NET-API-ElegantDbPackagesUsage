using Api.Extensions;
using Microsoft.AspNetCore.Components.Routing;
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

ConfigureLogger();
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


app.Run();

void ConfigureLogger()
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
        .WriteTo.Elasticsearch(ConfigureEls(configuration, environment))
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureEls(IConfiguration config, string env)
{
    return new ElasticsearchSinkOptions(new Uri(config.GetConnectionString("ElasticConnection") ??
                                                throw new Exception("Connection for elasticsearch not found")))
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