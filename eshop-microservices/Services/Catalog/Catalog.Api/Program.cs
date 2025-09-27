using System.Collections.Immutable;
using Catalog.Api;
using Catalog.Api.Infrastructure;
using HealthChecks.UI.Client;
using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var currentAssembly = typeof(Program).Assembly;
var connectionString = builder.Configuration.GetConnectionString("Postgres")
                       ?? throw new NullReferenceException("Postgres connection string is null");

builder.Services
    .AddMediator(currentAssembly)
    .AddCarterWithAssembly(currentAssembly)
    .AddMartenORM(builder.Configuration)
    .AddValidatorsFromAssembly(currentAssembly)
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddHealthChecks()
        .AddNpgSql(connectionString);;

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
