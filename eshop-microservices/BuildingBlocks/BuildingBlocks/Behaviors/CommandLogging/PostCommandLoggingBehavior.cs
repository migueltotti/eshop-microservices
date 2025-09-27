using LiteBus.Commands.Abstractions;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.CommandLogging;

public class PostCommandLoggingBehavior
    (ILogger<PreCommandLoggingBehavior> logger)
    : ICommandPostHandler
{

    public Task PostHandleAsync(ICommand message, object? messageResult,
        CancellationToken cancellationToken = new CancellationToken())
    {
        logger.LogInformation("[END] [{Time}] Handle request={Request} - result={Response}",
            DateTime.UtcNow, message.GetType(), messageResult);
        
        return Task.CompletedTask;
    }
}