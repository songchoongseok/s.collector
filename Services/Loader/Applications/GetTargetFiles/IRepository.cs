using Loader.Domains.Downloads;

namespace Loader.Applications.GetTargetFiles;

internal interface IRepository
{
    internal Task<Rule> GetRuleAsync(long id, CancellationToken cancellationToken);
}
