using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAndConfigureMediatR();

//Database configuration 
builder.Services.ConfigureDatabase(builder.Configuration);

//Repositories
builder.Services.AddCustomRepositories();

builder.Services.AddValidators();

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