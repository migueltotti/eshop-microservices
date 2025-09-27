using Catalog.Api.Exceptions;
using JasperFx.Core;

namespace Catalog.Api.Products.UpdateProduct;

public sealed record UpdateProductCommand(Product Product) : ICommand<UpdateProductResult>;

public sealed record UpdateProductResult(Product Product);

internal class UpdateProductCommandHandle(IDocumentSession session, ILogger<UpdateProductCommandHandle> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult> 
{
    public async Task<UpdateProductResult> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateProductCommandHandle.HandleAsync called with {@Command}", command);
        
        var product = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);

        if (product is null)
        {
            logger.LogInformation("UpdateProductCommandHandle.HandleAsync threw and error, {Error}",
                new { Error = "Product not found" });
            throw new ProductNotFoundException();
        }
        
        session.Update(command.Product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(command.Product);
    }
}