using FeatureFlags.Lite.Core.Abstractions;
using FeatureFlags.Lite.Core.Evaluation;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureFlags.Lite.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureFlagsCore(this IServiceCollection services)
        {
            services.AddSingleton<IFeatureEvaluator, DefaultFeatureEvaluator>();
            services.AddSingleton<IFeatureFlagService, FeatureFlagService>();

            return services;
        }
    }
}
