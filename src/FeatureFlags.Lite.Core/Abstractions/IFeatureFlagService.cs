using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.Core.Abstractions
{
    public interface IFeatureFlagService
    {
        bool IsEnabled(string featureName, FeatureContext? context = null);
    }
}