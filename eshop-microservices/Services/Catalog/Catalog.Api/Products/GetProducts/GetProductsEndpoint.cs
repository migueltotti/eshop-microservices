using LiteBus.Queries.Abstractions;

namespace Catalog.Api.Products.GetProducts;

public record GetAllProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetAllProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetAllProductsRequest request, IQueryMediator mediator) =>
        {
            var query = request.Adapt<GetProductsQuery>();

            var result = await mediator.QueryAsync(query);

            var products = result.Adapt<GetAllProductsResponse>();
            
            return Results.Ok(products);
        })
        .WithName("GetProducts")
        .Produces<GetAllProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get All Products");
    }
}