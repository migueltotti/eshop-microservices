using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.CommandLogging;

public class PostQueryLoggingBehavior
    (ILogger<PreCommandLoggingBehavior> logger)
    : IQueryPostHandler
{

    public Task PostHandleAsync(IQuery message, object? messageResult,
        CancellationToken cancellationToken = new CancellationToken())
    {
        logger.LogInformation("[END] [{Time}] Handle request={Request} - result={Response}",
            DateTime.UtcNow, message.GetType(), messageResult);
        
        return Task.CompletedTask;
    }
}