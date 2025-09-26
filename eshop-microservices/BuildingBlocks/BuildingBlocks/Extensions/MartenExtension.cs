using JasperFx;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Extensions;

public static class MartenExtension
{
    public static IServiceCollection AddMartenORM(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new NullReferenceException("Postgres connection string is null");
        
        services.AddMarten(opts =>
        {
            opts.Connection(connectionString);
        }).UseLightweightSessions();
        
        return services;
    }
}