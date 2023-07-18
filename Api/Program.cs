using Api.Extensions;

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
builder.Services.AddMediatRNotifications();
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