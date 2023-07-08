namespace Api.Endpoints;

public static class EndpointsConfiguration
{
    public static void ConfigureEndpoints(this IApplicationBuilder app)
    {
        app.UseRouting();
        
    }
}