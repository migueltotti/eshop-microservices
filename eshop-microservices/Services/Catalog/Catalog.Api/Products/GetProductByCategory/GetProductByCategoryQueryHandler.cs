namespace Catalog.Api.Products.GetProductByCategory;

public sealed record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public sealed record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> HandleAsync(GetProductsByCategoryQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.HandleAsync called with {@Query}", query);
        
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        
        var result = new GetProductsByCategoryResult(products);

        return result;
    }
}