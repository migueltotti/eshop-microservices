namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler()
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> HandleAsync(GetBasketQuery message, CancellationToken cancellationToken = default)
    {
        // TODO: get basket from database
        return new GetBasketResult(new ShoppingCart("MockShoppingCart"));
    }
}