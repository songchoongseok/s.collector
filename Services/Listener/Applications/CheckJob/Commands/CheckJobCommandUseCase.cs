using System.Collections;
using MediatR;
using Quartz.Util;

namespace Listener.Applications.CheckJob.Commands;

internal sealed class CheckJobCommandUseCase(ILogger<CheckJobCommandUseCase> logger,
    IRepository repository) : IRequestHandler<CheckJobCommand>
{
    private readonly ILogger<CheckJobCommandUseCase> _logger = logger;
    private readonly IRepository _repository = repository;

    public async Task Handle(CheckJobCommand command, CancellationToken cancellationToken)
    {
        var nextWorkingTimeByRuleId = await CheckNextJobAsync(cancellationToken);

        _logger.LogInformation("nextJobCount: {count}", nextWorkingTimeByRuleId.Count);

        await CreateJobs(nextWorkingTimeByRuleId, cancellationToken);

        _logger.LogInformation("end");
    }

    private async Task CreateJobs(Dictionary<long, DateTime> nextWorkingTimeByRuleId, CancellationToken cancellationToken)
    {
        foreach (var data in nextWorkingTimeByRuleId)
        {
            await _repository.CreateJobAsync(data.Key, cancellationToken);
            await _repository.UpdateLastWorkingTimeOfRuleAsync(data.Key, data.Value, cancellationToken);
        }
    }

    private async Task<Dictionary<long, DateTime>> CheckNextJobAsync(CancellationToken cancellationToken)
    {
        var nextWorkingTimeByRuleId = new Dictionary<long, DateTime>();
        var rules = await _repository.GetRulesAsync(cancellationToken);

        _logger.LogInformation("ruleCount: {count}", rules.Count);

        var now = DateTime.Now;
        foreach (var rule in rules)
        {
            if (rule.NextWorkingTime() < now)
                nextWorkingTimeByRuleId.Add(rule.Id, rule.NextWorkingTime());
        }

        return nextWorkingTimeByRuleId;
    }
}
