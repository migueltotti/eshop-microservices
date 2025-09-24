using Catalog.Api.Products.CreateProduct;
using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;
using LiteBus.Queries.Extensions.MicrosoftDependencyInjection;

namespace Catalog.Api.Extensions;

public static class MediatorExtension
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        var assembly = typeof(CreateProductCommand).Assembly;

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
        });

        return services;
    }
}