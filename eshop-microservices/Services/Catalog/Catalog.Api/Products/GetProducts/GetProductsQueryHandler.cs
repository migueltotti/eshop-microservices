using LiteBus.Queries.Abstractions;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetProductsQueryHandler.HandleAsync called with {@Query}", query);

        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}