using Microsoft.Extensions.Options;

namespace Listener.Abstractions.Options.QuartzOptions;

internal sealed class QuartzOptionsSetup(IConfiguration configuration) : IConfigureOptions<QuartzOptions>
{
    private readonly IConfiguration _configuration = configuration;

    private const string _configurationSectionName = nameof(QuartzOptions);

    public void Configure(QuartzOptions options)
    {
        _configuration.GetSection(_configurationSectionName)
                      .Bind(options);
    }
}
