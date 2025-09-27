using System.Collections.Immutable;
using Catalog.Api;
using Catalog.Api.Infrastructure;
using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var currentAssembly = typeof(Program).Assembly;

builder.Services
    .AddMediator(currentAssembly)
    .AddCarterWithAssembly(currentAssembly)
    .AddMartenORM(builder.Configuration)
    .AddValidatorsFromAssembly(currentAssembly);

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(option => { });

app.Run();
