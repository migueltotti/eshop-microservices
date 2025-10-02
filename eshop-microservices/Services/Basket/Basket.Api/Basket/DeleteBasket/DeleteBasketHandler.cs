using Microsoft.CodeAnalysis.Operations;

namespace Basket.Api.Basket.DeleteBasket;

public sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public sealed record DeleteBasketResult(bool IsSuccess);

public sealed class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
                .WithMessage("UserName is required!");
    }
}
    
public class DeleteBasketCommandHandler(
    IBasketRepository repository,
    IValidator<DeleteBasketCommand> validator)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> HandleAsync(DeleteBasketCommand message, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowExceptionIfNotValidAsync(message, cancellationToken);
        
        var result = await repository.DeleteBasketAsync(message.UserName, cancellationToken);
        
        return new DeleteBasketResult(result);
    }
}