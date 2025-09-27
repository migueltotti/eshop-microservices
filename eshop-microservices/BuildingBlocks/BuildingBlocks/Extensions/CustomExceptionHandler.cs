using LiteBus.Messaging.Internal.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Extensions;

public static class CustomExceptionHandler
{
    public static WebApplication UseCustomExceptionHandler<TReference>(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (exception is null)
                    return;

                var problemsDetails = new ProblemDetails
                {
                    Title = exception.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = exception.StackTrace,
                };

                var logger = context.RequestServices.GetRequiredService<ILogger<TReference>>();
                logger.LogError(exception, exception.Message);
                
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                
                await context.Response.WriteAsJsonAsync(problemsDetails);
            });
        });

        return app;
    }
}