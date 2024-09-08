using Listener.Abstractions.Options.InMemoryOptions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class AdapterRegistrations
{
    public static IServiceCollection RegisterAdapters(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppSettingsOptions()
                .AddWorkers()
                .AddLog(configuration);

        // builder.Services
        //         .AddOptions<InMemoryOptions>()
        //         .Bind(builder.Configuration.GetSection(ApplicationOptions.Key));

        // if (inMemoryOptions.Value.IsInMemory)
        // {

        // }



        return services;
    }

    private static IServiceCollection AddAppSettingsOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<InMemoryOptionsSetup>();
        services.AddSingleton<IValidateOptions<InMemoryOptions>, InMemoryOptionsValidator>();

        return services;
    }

    private static IServiceCollection AddWorkers(this IServiceCollection services)
    {

        return services;
    }

    private static IServiceCollection AddLog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(options =>
        {
            options.ReadFrom.Configuration(configuration);
        });

        return services;
    }
}
