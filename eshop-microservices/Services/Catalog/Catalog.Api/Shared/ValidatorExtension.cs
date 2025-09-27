using System.Runtime.InteropServices.JavaScript;
using FluentValidation.Results;

namespace Catalog.Api.Shared;

public static class ValidatorExtension
{
    public static async Task ValidateAndThrowExceptionIfNotValidAsync<TRequest>(this IValidator<TRequest> validator, 
        TRequest request, CancellationToken cancellationToken = default)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        
        if(!result.IsValid)
            throw new ValidationException(result.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
    }
}