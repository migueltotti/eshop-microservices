namespace Catalog.Api.Products.UpdateProduct;

public sealed record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price);

public sealed record UpdateProductResponse(Product Product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async ([FromBody] UpdateProductRequest request, ICommandMediator commandMediator) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            
            var result = await commandMediator.SendAsync(command);

            var response = result.Adapt<UpdateProductResponse>();
            
            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update one Product")
        .WithDescription("Update one Product");
    }
}