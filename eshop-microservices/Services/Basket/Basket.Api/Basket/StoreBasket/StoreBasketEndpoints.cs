using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(bool IsSuccess);

public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async ([FromBody] ShoppingCart cart, ICommandMediator commandMediator) =>
        {
            var result = await commandMediator.SendAsync(new StoreBasketCommand(cart));

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Ok(response);
        });
    }
}