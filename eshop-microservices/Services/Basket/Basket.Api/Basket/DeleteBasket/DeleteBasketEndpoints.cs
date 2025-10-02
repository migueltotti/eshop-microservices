namespace Basket.Api.Basket.DeleteBasket;

public sealed record DeleteBasketRequest(string UserName);
public sealed record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ICommandMediator commandMediator) =>
        {
            var result = await commandMediator.SendAsync(new DeleteBasketCommand(userName));

            var response = result.Adapt<DeleteBasketResponse>();
            
            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket by UserName");
    }
}