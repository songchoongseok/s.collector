using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

builder.Services.RegisterAdapters(builder.Configuration)
                .RegisterApplications();

var host = builder.Build();
host.Run();