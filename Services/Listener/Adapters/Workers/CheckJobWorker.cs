using MediatR;
using Quartz;

namespace Listener.Adapters.Workers;

[DisallowConcurrentExecution]
public sealed class CheckJobWorker(ILogger<CheckJobWorker> logger, IMediator mediator) : IJob, IDisposable
{
    private readonly ILogger<CheckJobWorker> _logger = logger;
    private readonly IMediator _mediator = mediator;

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Worker running at : {time}", DateTime.Now);

        await Task.Delay(5000);
    }

    public void Dispose()
    {
    }
}
