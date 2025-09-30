namespace Catalog.Api.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public sealed record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("Product Id is required");
    }
}

public class DeleteProductCommandHandler(
    IDocumentSession session,
    IValidator<DeleteProductCommand> validator,
    ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        logger.LogInformation("DeleteProductCommandHandler.HandleAsync called with {@Command}", command);
        
        await validator.ValidateAndThrowExceptionIfNotValidAsync(command, cancellationToken);
        
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}