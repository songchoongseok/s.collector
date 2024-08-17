using Domains;

namespace Applications;

public interface ILocalStorage
{
    public Task<IEnumerable<string>> GetRuleFilesAsync(CancellationToken cancellationToken);
    public Task<Rule> ReadRuleAsync(string ruleFile, CancellationToken cancellationToken);
}