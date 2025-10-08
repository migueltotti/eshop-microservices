using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var currentAssembly = typeof(Program).Assembly;
var dbConnectionString = builder.Configuration.GetConnectionString("Postgres")
                       ?? throw new NullReferenceException("Postgres connection string is null");
var redisConnectionString = builder.Configuration.GetConnectionString("Redis")
                            ?? throw new NullReferenceException("Redis connection string is null");

builder.Services
    .AddMediator(currentAssembly)
    .AddCarterWithAssembly(currentAssembly)
    .AddValidatorsFromAssembly(currentAssembly)
    .AddMarten(opts =>
    {
        opts.Connection(dbConnectionString);
        opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    }).UseLightweightSessions();

builder.Services
    .AddScoped<IBasketRepository, BasketRepository>()
        .Decorate<IBasketRepository, CachedBasketRepository>()
    .AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnectionString;
    });

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConnectionString)
    .AddRedis(redisConnectionString);

var app = builder.Build();

// Configure the HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();