using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureApp
{
    public static IApplicationBuilder AddApplicationApps(this IApplicationBuilder app)
    {
        app.UseMiddleware<PerformanceMiddleware>();

        return app;
    }
}
