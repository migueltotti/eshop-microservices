namespace Catalog.Api.Products.GetProductByCategory;

//public sealed record GetProductsByCategoryRequest(string Catgory);
public sealed record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async ([FromRoute] string category, IQueryMediator queryMediator) =>
        {
            var result = await queryMediator.QueryAsync(new GetProductsByCategoryQuery(category));

            var response = result.Adapt<GetProductsByCategoryResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get a list of products by Category")
        .WithDescription("Get a list of products by Category");
    }
}