namespace Catalog.Api.Products.GetProductById;


//public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async ([FromRoute] Guid id, IQueryMediator queryMediator) =>
        {
            var result = await queryMediator.QueryAsync(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .WithSummary("Get a product by id")
        .WithDescription("Get a product by id");
    }
}