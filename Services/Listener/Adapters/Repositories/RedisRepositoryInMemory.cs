using Listener.Applications.CheckJob;
using Listener.Domains.Rules;
using Listener.Domains.Rules.ValueObjects;

namespace Listener.Adapters.Repositories;

public class RedisRepositoryInMemory : IRepository
{
    private List<long> _jobs = [];
    private Dictionary<long, Rule> _rules = new Dictionary<long, Rule>()
    {
        {1, new Rule(1, ScheduleType.Minute, 5, DateTime.Now)},
        {2, new Rule(2, ScheduleType.Second, 6, DateTime.Now)},
        {3, new Rule(3, ScheduleType.Minute, 5, DateTime.Now)},
        {4, new Rule(4, ScheduleType.Minute, 5, DateTime.Now)},
    };

    public async Task CreateJobAsync(long ruleId, CancellationToken cancellationToken)
    {
        await Task.Delay(5);å
        _jobs.Add(ruleId);

        return;
    }

    public async Task<IList<Rule>> GetRulesAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(5);

        return _rules.Values.ToList();
    }

    public async Task UpdateLastWorkingTimeOfRuleAsync(long ruleId, DateTime lastWorkingTime, CancellationToken cancellationToken)
    {
        await Task.Delay(5);

        if (!_rules.ContainsKey(ruleId))
            return;

        var rule = _rules[ruleId];
        var newRule = new Rule(ruleId, rule.ScheduleType, rule.ScheduleTime, lastWorkingTime);

        _rules[ruleId] = newRule;
    }
}
