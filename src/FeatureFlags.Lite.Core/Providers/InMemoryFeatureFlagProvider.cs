using FeatureFlags.Lite.Core.Abstractions;
using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.Core.Providers
{
    public sealed class InMemoryFeatureFlagProvider : IFeatureFlagProvider
    {
        private readonly IReadOnlyDictionary<string, FeatureFlag> _flags;

        public InMemoryFeatureFlagProvider(IEnumerable<FeatureFlag> flags)
        {
            _flags = flags.ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase);
        }

        public FeatureFlag? Get(string featureName)
            => _flags.TryGetValue(featureName, out var flag) ? flag : null;
    }
}
