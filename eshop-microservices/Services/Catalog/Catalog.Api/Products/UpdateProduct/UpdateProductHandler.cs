using Catalog.Api.Shared;

namespace Catalog.Api.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;

public sealed record UpdateProductResult(Product Product);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("Name is required")
            .Length(2, 150)
                .WithMessage("Name must be between 2 and 150 characters");
        RuleFor(p => p.Price)
            .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
    }
}

internal class UpdateProductCommandHandle(
    IDocumentSession session, 
    ILogger<UpdateProductCommandHandle> logger,
    IValidator<UpdateProductCommand> validator)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult> 
{
    public async Task<UpdateProductResult> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateProductCommandHandle.HandleAsync called with {@Command}", command);
        
        await validator.ValidateAndThrowExceptionIfNotValidAsync(command, cancellationToken);
        
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            logger.LogInformation("UpdateProductCommandHandle.HandleAsync threw and error, {Error}",
                new { Error = "Product not found" });
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(product);
    }
}