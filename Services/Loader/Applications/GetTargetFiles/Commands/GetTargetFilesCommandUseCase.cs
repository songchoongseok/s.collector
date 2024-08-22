using Loader.Domains.Downloads;
using MediatR;

namespace Loader.Applications.GetTargetFiles.Commands;

internal sealed class GetTargetFilesCommandUseCase(
    ILogger<GetTargetFilesCommandUseCase> logger,
    IRepository repository,
    IFtpClient ftpClient
) : IRequestHandler<GetTargetFilesCommand>
{
    private readonly ILogger<GetTargetFilesCommandUseCase> _logger = logger;
    private readonly IRepository _repository = repository;
    private readonly IFtpClient _ftpClient = ftpClient;

    public async Task Handle(GetTargetFilesCommand command, CancellationToken cancellationToken)
    {
        Rule rule = await _repository.GetRuleAsync(command.Id, cancellationToken);

        _logger.LogInformation("Get Rule. Id: {id}, Name: {Name}, TargetCount:{Count}", rule.Id, rule.Name, rule.Count());

    }
}
