using Catalog.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddMediator()
    .AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
