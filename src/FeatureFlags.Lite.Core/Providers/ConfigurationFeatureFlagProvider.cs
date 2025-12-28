using FeatureFlags.Lite.Core.Models;
using Microsoft.Extensions.Configuration;

namespace FeatureFlags.Lite.Core.Abstractions
{
    public sealed class ConfigurationFeatureFlagProvider : IFeatureFlagProvider
    {
        private readonly IConfiguration _configuration;

        public ConfigurationFeatureFlagProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FeatureFlag? Get(string featureName)
        {
            var section = _configuration.GetSection($"FeatureFlags:{featureName}");
            if (!section.Exists()) return null;

            var flag = section.Get<FeatureFlag>();
            if (flag == null) return null;

            return new FeatureFlag
            {
                Name = featureName,
                Enabled = flag.Enabled,
                RolloutPercentage = flag.RolloutPercentage,
                AllowedRoles = flag.AllowedRoles,
                EnvironmentOverrides = flag.EnvironmentOverrides
            };
        }
    }
}
