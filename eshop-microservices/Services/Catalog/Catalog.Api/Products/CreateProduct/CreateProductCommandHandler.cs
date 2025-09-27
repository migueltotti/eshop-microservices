using Catalog.Api.Shared;
using FluentValidation;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
    ) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name is required");
        RuleFor(x => x.Categories)
            .NotEmpty()
                .WithMessage("Category is required");
        RuleFor(x => x.ImageFile)
            .NotEmpty()
                .WithMessage("ImageFile is required");
        RuleFor(x => x.Price)
            .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator,ILogger<CreateProductCommand> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CreateProductCommandHandler.HandleAsync called with {@Command}", command);
        
        await validator.ValidateAndThrowExceptionIfNotValidAsync(command, cancellationToken);
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Category = command.Categories,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}