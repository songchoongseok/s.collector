using MediatR;
using Domains;

namespace Applications.RegisterRule;

public sealed class RegisterRuleCommandUsecase(
    ILogger<RegisterRuleCommandUsecase> logger,
    ILocalStorage localStorage,
    IRepository repository
) : IRequestHandler<RegisterRuleCommand>
{
    private readonly ILogger<RegisterRuleCommandUsecase> _logger = logger;
    private readonly ILocalStorage _localStorage = localStorage;
    private readonly IRepository _repository = repository;

    public async Task Handle(RegisterRuleCommand request, CancellationToken cancellationToken)
    {
        var rules = await GetRulesAsync(cancellationToken);
        await _repository.SetRulesAsync(rules, cancellationToken);

        _logger.LogInformation("Get rules. ruleCount: {ruleCount}", rules.Count());
    }

    private async Task<IEnumerable<Rule>> GetRulesAsync(CancellationToken cancellationToken)
    {
        List<Rule> rules = [];

        var ruleFiles = await _localStorage.GetRuleFilesAsync(cancellationToken);
        foreach (var ruleFile in ruleFiles)
            rules.Add(await _localStorage.ReadRuleAsync(ruleFile, cancellationToken));

        return rules;
    }
}