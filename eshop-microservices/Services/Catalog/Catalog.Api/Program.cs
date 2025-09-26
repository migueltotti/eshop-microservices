var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var currentAssembly = typeof(Program).Assembly;

builder.Services
    .AddMediator(currentAssembly)
    .AddCarterWithAssembly(currentAssembly)
    .AddMartenORM(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
