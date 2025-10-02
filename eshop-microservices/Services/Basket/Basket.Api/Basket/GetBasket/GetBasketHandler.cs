namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> HandleAsync(GetBasketQuery message,
        CancellationToken cancellationToken = default)
    {
        var basket = await repository.GetBasketAsync(message.UserName, cancellationToken);
        
        return new GetBasketResult(basket);
    }
}