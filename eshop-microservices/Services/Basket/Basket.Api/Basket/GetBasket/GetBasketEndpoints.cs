using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Basket.GetBasket;

//public record GetBasketRequest(string UserName);
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{user-name}",
            async ([FromRoute(Name = "user-name")] string userName, IQueryMediator queryMediator) =>
        {
                var result = await queryMediator.QueryAsync(new GetBasketQuery(userName));

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket")
        .WithDescription("Get Basket by UserName");
    }
}