namespace Catalog.Api.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
    );

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", 
            async (CreateProductRequest request, ICommandMediator commandMediator) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await commandMediator.SendAsync(command);

                var response = result.Adapt<CreateProductResponse>();
                
                return Results.Created($"/product/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .WithSummary("Creates a product")
            .WithDescription("Create a product by its request");
    }
}