using Listener.Domains.Rules;

namespace Listener.Applications.CheckJob;

public interface IRepository
{
    public Task<IList<Rule>> GetRulesAsync(CancellationToken cancellationToken);
    public Task UpdateLastWorkingTimeOfRuleAsync(long ruleId, DateTime lastWorkingTime, CancellationToken cancellationToken);
    public Task CreateJobAsync(long ruleId, CancellationToken cancellationToken);
}
