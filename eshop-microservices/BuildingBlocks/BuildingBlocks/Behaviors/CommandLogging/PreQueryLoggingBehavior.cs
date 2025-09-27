using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors.CommandLogging;

public class PreQueryLoggingBehavior
    (ILogger<PreCommandLoggingBehavior> logger)
    : IQueryPreHandler
{
    public Task PreHandleAsync(IQuery message, CancellationToken cancellationToken = new CancellationToken())
    {
        logger.LogInformation("[Start] [{Time}] Handle request={Request} - RequestData={RequestData}",
            DateTime.UtcNow, message.GetType(), message);
        
        return Task.CompletedTask;
    }
}