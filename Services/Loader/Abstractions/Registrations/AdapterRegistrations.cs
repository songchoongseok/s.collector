
using Loader.Abstractions.Options.InMemoryOptions;
using Microsoft.Extensions.Options;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class AdapterRegistrations
{
    public static IServiceCollection RegisterAdapters(this IServiceCollection services
    , IConfiguration configuration)
    {
        // builder.Services
        //         .AddOptions<InMemoryOptions>()
        //         .Bind(builder.Configuration.GetSection(ApplicationOptions.Key));

        // if (inMemoryOptions.Value.IsInMemory)
        // {

        // }

        services.AddSerilog(options =>
        {
            options.ReadFrom.Configuration(configuration);
        });

        return services;
    }
}
