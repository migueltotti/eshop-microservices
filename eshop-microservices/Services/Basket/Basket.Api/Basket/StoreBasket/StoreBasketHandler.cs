namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull()
                .WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName)
            .NotEmpty()
            .WithMessage("UserName ir required");
    }
}

public class StoreBasketCommandHandler(
    IBasketRepository repository,
    IValidator<StoreBasketCommand> validator) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> HandleAsync(StoreBasketCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        await validator.ValidateAndThrowExceptionIfNotValidAsync(command, cancellationToken);
        
        // TODO: upsert-create (if the document exists, update - if not, create) Basket in database
        // TODO: update cache 

        var cart = command.Cart;

        await repository.StoreBasketAsync(cart, cancellationToken);
        
        return new StoreBasketResult(command.Cart.UserName);
    }
}