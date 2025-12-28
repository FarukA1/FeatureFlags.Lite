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

            bool? enabled = null;
            int? rolloutPercentage = null;
            List<string>? allowedRoles = null;
            Dictionary<string, bool>? environmentOverrides = null;

            foreach(var childSection in section.GetChildren())
            {
                switch (childSection.Key)
                {
                    case nameof(FeatureFlag.Enabled):
                        enabled = childSection.Get<bool>();
                        break;
                    case nameof(FeatureFlag.RolloutPercentage):
                        rolloutPercentage = childSection.Get<int?>();
                        break;
                    case nameof(FeatureFlag.AllowedRoles):
                        allowedRoles = childSection.Get<List<string>>();
                        break;
                    case nameof(FeatureFlag.EnvironmentOverrides):
                        environmentOverrides = childSection.Get<Dictionary<string, bool>>();
                        break;
                }
            }

            return new FeatureFlag
            {
                Name = featureName,
                Enabled = enabled ?? false,
                RolloutPercentage = rolloutPercentage,
                AllowedRoles = allowedRoles,
                EnvironmentOverrides = environmentOverrides
            };
        }
    }
}
