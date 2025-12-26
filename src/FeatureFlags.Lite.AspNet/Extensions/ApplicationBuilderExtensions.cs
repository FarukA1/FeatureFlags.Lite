using FeatureFlags.Lite.AspNet.Context;
using FeatureFlags.Lite.AspNet.Middleware;
using FeatureFlags.Lite.Core.Extensions;
using FeatureFlags.Lite.Core.Providers;

namespace FeatureFlags.Lite.AspNet.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseFeatureFlags(this IApplicationBuilder app)
        {
            app.UseMiddleware<FeatureFlagsMiddleware>();
            return app;
        }

        public static IServiceCollection AddFeatureFlags(this IServiceCollection services)
        {
            services.AddFeatureFlagsCore();
            services.AddSingleton<IHttpContextFeatureContextFactory, HttpContextFeatureContextFactory>();
            services.AddSingleton<InMemoryFeatureFlagProvider>();
            return services;
        }
    }
}