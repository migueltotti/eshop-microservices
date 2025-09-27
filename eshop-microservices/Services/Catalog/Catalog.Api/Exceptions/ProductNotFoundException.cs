

namespace Catalog.Api.Exceptions;

public class ProductNotFoundException(Guid guid) : NotFoundException("Product", guid);