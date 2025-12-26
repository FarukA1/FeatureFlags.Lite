using FeatureFlags.Lite.Core.Abstractions;
using FeatureFlags.Lite.Core.Evaluation;
using FeatureFlags.Lite.Core.Models;
using FeatureFlags.Lite.Core.Providers;
using Moq;

namespace FeatureFlags.Lite.Core.Tests
{
    public class FeatureFlagServiceTests
    {

        [Fact]
        public void TestFeatureFlagService()
        {
            var featureFlagProvider = new InMemoryFeatureFlagProvider(new[]
            {
                new FeatureFlag { Name = "FeatureA", Enabled = true },
                new FeatureFlag { Name = "FeatureB", Enabled = false }
            });

            var evaluator = new DefaultFeatureEvaluator();
            var service = new FeatureFlagService(featureFlagProvider, evaluator);

            var isFeatureAEnabled = service.IsEnabled("FeatureA");
            var isFeatureBEnabled = service.IsEnabled("FeatureB");
            var isNonExistentFeatureEnabled = service.IsEnabled("FeatureC");

            Assert.True(isFeatureAEnabled);
            Assert.False(isFeatureBEnabled);
            Assert.False(isNonExistentFeatureEnabled);
        }
    }
}