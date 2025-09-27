using Catalog.Api.Exceptions;
using LiteBus.Queries.Abstractions;

namespace Catalog.Api.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public sealed record GetProductByIdResult(Product Product); 

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> HandleAsync(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetProductByIdQueryHandler.HandleAsync called with {@Query}", query);
        
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}