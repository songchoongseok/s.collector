using Domains;
using Applications;

namespace Adapters.LocalStorage;

public sealed class LocalStorageInMemory : ILocalStorage
{
    public async Task<IEnumerable<string>> GetRuleFilesAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100);
        return ["RULE1", "RULE2", "RULE3"];
    }
    public async Task<Rule> ReadRuleAsync(string ruleFile, CancellationToken cancellationToken)
    {
        await Task.Delay(100);

        Rule rule = new(1, "RULE1", new("127.0.0.1", "test", "test", 21));
        rule.Add("target1");
        rule.Add("target2");
        rule.Add("target3");
        return rule;
    }
}