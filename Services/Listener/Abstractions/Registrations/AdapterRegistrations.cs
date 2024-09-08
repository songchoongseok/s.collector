using Listener.Abstractions.Options.InMemoryOptions;
using Listener.Abstractions.Options.QuartzOptions;
using Listener.Adapters.Repositories;
using Listener.Adapters.Workers;
using Listener.Applications.CheckJob;
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
                .AddLog(configuration)
                .AddRepositories(configuration);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(InMemoryOptions)).Get<InMemoryOptions>()!;

        if(options.IsInMemory)
        {
            services.AddSingleton<IRepository, RedisRepositoryInMemory>();
        }
        else
        {}

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
