using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.Core.Abstractions
{
    public interface IFeatureFlagProvider
    {
        FeatureFlag? Get(string featureName);
    }
}
