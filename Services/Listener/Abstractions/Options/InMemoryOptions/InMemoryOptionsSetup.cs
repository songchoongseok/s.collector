using Microsoft.Extensions.Options;

namespace Listener.Abstractions.Options.InMemoryOptions;

internal sealed class InMemoryOptionsSetup(IConfiguration configuration) : IConfigureOptions<InMemoryOptions>
{
    private readonly IConfiguration _configuration = configuration;

    private const string _configurationSectionName = nameof(InMemoryOptions);

    public void Configure(InMemoryOptions options)
    {
        _configuration.GetSection(_configurationSectionName)
                      .Bind(options);
    }
}


