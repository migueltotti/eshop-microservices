using System.Reflection;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Behaviors.CommandLogging;
using LiteBus.Commands.Abstractions;
using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;
using LiteBus.Queries.Extensions.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Extensions;

public static class MediatorExtension
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddLiteBus(liteBus =>
        {
            liteBus.AddCommandModule(module =>
            {
                module.RegisterFromAssembly(assembly);
            });

            liteBus.AddQueryModule(module =>
            {
                module.RegisterFromAssembly(assembly);
            });

            // Add Command Logging
            liteBus.AddCommandModule(module =>
            {
                module.Register<PreCommandLoggingBehavior>();
                module.Register<PostCommandLoggingBehavior>();
            });
            
            // Add Query Logging
            liteBus.AddQueryModule(module =>
            {
                module.Register<PreQueryLoggingBehavior>();
                module.Register<PostQueryLoggingBehavior>();
            });
        });

        return services;
    }
}