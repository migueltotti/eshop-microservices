using Catalog.Api.Models;
using LiteBus.Commands.Abstractions;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
    ) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> HandleAsync(CreateProductCommand message, CancellationToken cancellationToken = default)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = message.Name,
            Description = message.Description,
            Category = message.Categories,
            ImageFile = message.ImageFile,
            Price = message.Price
        };
        
        // TODO: save to database
        
        return new CreateProductResult(product.Id);
    }
}