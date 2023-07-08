using Application.Features.EfCoreFeatures;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class EfCoreEndpoints
{
    public static void MapEfCoreEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/products", () => "zxczxc");

            endpoints.MapGet("/products/{id}", GetProductById);


            endpoints.MapPost("/products", CreateProduct);


            endpoints.MapPut("/products/{id}", UpdateProduct);

            endpoints.MapDelete("/products/{id}", DeleteProduct);
        });
    }

    private static List<Photo> products = new List<Photo>();

    private static async Task<Photo> GetAllProducts(HttpContext context)
    {
        var a = context.RequestServices.GetService<IMediator>();
        await a.Send(new AddPhotoRequest(new Photo()));
        var productsJson = System.Text.Json.JsonSerializer.Serialize(products);

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(productsJson);
        return new Photo();
    }

    private static Task GetProductById(HttpContext context)
    {
        return Task.CompletedTask;
    }

    private static async void CreateProduct(HttpContext context)
    {
    }

    private static async void UpdateProduct(HttpContext context)
    {
    }

    private static void DeleteProduct()
    {
    }
}

public static class ProductEndpointExtensions
{
    public static void AddProductEndpoints(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
    }
}