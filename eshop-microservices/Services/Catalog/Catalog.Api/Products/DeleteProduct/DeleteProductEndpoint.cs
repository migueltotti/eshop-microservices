namespace Catalog.Api.Products.DeleteProduct;

//public sealed record DeleteProductRequest(string Id);
public sealed record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async ([FromRoute] Guid id, ICommandMediator commandMediator) =>
        {
            var result = await commandMediator.SendAsync(new DeleteProductCommand(id));

            var response = result.Adapt<DeleteProductResponse>();
            
            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Deletes a product")
        .WithDescription("Deletes a product");
    }
}