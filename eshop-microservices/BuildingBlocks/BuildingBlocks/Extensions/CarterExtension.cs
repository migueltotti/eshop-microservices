using System.Reflection;
using Carter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace BuildingBlocks.Extensions;

public static class CarterExtension
{
    public static IServiceCollection AddCarterWithAssembly(this IServiceCollection services, Assembly assembly)
    {
        services.AddCarter(
            new DependencyContextAssemblyCatalog(assemblies: assembly)
        );

        return services;
    }
}