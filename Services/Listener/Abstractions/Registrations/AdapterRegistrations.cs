using Listener.Abstractions.Options.InMemoryOptions;
using Listener.Abstractions.Options.QuartzOptions;
using Listener.Adapters.Workers;
using Microsoft.Extensions.Options;
using Quartz;
using Serilog;

using QuartzOptions = Listener.Abstractions.Options.QuartzOptions.QuartzOptions;

namespace Microsoft.Extensions.DependencyInjection;

public static class AdapterRegistrations
{
    public static IServiceCollection RegisterAdapters(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppSettingsOptions()
                .AddWorkers(configuration)
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

        services.ConfigureOptions<QuartzOptionsSetup>();
        services.AddSingleton<IValidateOptions<QuartzOptions>, QuartzOptionsValidator>();

        return services;
    }

    private static IServiceCollection AddWorkers(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(QuartzOptions)).Get<QuartzOptions>()!;

        services.AddQuartz(q =>
        {
            JobKey checkJobWorkerKey = JobKey.Create(nameof(CheckJobWorker));
            q.AddJob<CheckJobWorker>(builder => builder.WithIdentity(checkJobWorkerKey))
            .AddTrigger(trigger => trigger
                        .ForJob(checkJobWorkerKey)
                        .WithIdentity(nameof(CheckJobWorker) + "-Trigger")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(options.Seconds)
                            .RepeatForever()));
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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
