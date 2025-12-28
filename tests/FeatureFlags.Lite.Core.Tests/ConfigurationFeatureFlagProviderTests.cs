using FeatureFlags.Lite.Core.Abstractions;
using Microsoft.Extensions.Configuration;

namespace FeatureFlags.Lite.Core.Tests
{
    public class ConfigurationFeatureFlagProviderTests
    {
        [Fact]
        public void TestLoadFeatureFlagsFromConfigurationExists()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"FeatureFlags:TestFeature:Enabled", "true"},
                {"FeatureFlags:TestFeature:AllowedUsers:0", "user1"},
                {"FeatureFlags:TestFeature:RolloutPercentage", "50"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var provider = new ConfigurationFeatureFlagProvider(configuration);
            var featureFlag = provider.Get("TestFeature");

            Assert.NotNull(featureFlag);
            Assert.True(featureFlag.Enabled);
            Assert.Equal(50, featureFlag.RolloutPercentage);
        }

        [Fact]
        public void TestLoadFeatureFlagsFromConfigurationNotFound()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .Build();

            var provider = new ConfigurationFeatureFlagProvider(configuration);
            var featureFlag = provider.Get("NonExistentFeature");

            Assert.Null(featureFlag);
        }
    }
}