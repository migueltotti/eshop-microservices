using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var currentAssembly = typeof(Program).Assembly;
var connectionString = builder.Configuration.GetConnectionString("Postgres")
                       ?? throw new NullReferenceException("Postgres connection string is null");

builder.Services
    .AddMediator(currentAssembly)
    .AddCarterWithAssembly(currentAssembly)
    .AddValidatorsFromAssembly(currentAssembly)
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddMarten(opts =>
    {
        opts.Connection(connectionString);
        opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    }).UseLightweightSessions();

builder.Services
    .AddScoped<IBasketRepository, BasketRepository>()
        .Decorate<IBasketRepository, CachedBasketRepository>()
    .AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis")
                                ?? throw new NullReferenceException("Redis connection string is null");
    });

var app = builder.Build();

// Configure the HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler(option => { });

app.Run();