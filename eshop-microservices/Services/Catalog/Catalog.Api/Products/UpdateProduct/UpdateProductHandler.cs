using Catalog.Api.Exceptions;
using JasperFx.Core;

namespace Catalog.Api.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;

public sealed record UpdateProductResult(Product Product);

internal class UpdateProductCommandHandle(IDocumentSession session, ILogger<UpdateProductCommandHandle> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult> 
{
    public async Task<UpdateProductResult> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateProductCommandHandle.HandleAsync called with {@Command}", command);
        
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            logger.LogInformation("UpdateProductCommandHandle.HandleAsync threw and error, {Error}",
                new { Error = "Product not found" });
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Categories;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(product);
    }
}