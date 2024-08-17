using Domains;

namespace Applications;

public interface IRepository
{
    public Task SetRulesAsync(IEnumerable<Rule> rules, CancellationToken cancellationToken);
}
