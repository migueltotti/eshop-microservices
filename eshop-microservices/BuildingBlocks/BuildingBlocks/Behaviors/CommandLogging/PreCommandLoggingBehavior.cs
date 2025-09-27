using LiteBus.Commands.Abstractions;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.CommandLogging;

public class PreCommandLoggingBehavior
    (ILogger<PreCommandLoggingBehavior> logger)
    : ICommandPreHandler
{
    public Task PreHandleAsync(ICommand message, CancellationToken cancellationToken = new CancellationToken())
    {
        logger.LogInformation("[Start] [{Time}] Handle request={Request} - RequestData={RequestData}",
            DateTime.UtcNow, message.GetType(), message);
        
        return Task.CompletedTask;
    }
}