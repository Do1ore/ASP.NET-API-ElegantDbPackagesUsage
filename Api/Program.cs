using System.Reflection;
using Api;
using Api.Endpoints;
using Api.EndpointsExtensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEfCoreEndpoints();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Minimal Api endpoint configuration
app.ConfigureEndpoints();

app.UseHttpsRedirection();
app.MapEfCoreEndpoints();

app.Run();