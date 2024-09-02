using Listener;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationRegistrations
{
    public static IServiceCollection RegisterApplications(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        return services;
    }
}
