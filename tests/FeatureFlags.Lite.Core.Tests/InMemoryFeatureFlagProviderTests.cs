using FeatureFlags.Lite.Core.Models;
using FeatureFlags.Lite.Core.Providers;

namespace FeatureFlags.Lite.Core.Tests
{
    public class InMemoryFeatureFlagProviderTests
    {
        [Fact]
        public void TestInMemoryFeatureFlagProviderExists()
        {
            var flag = new FeatureFlag { Name = "FeatureA", Enabled = true };
            var provider = new InMemoryFeatureFlagProvider(new[] { flag });

            var result = provider.Get("FeatureA");

            Assert.NotNull(result);
            Assert.Equal(flag.Name, result.Name);
        }

        [Fact]
        public void TestInMemoryFeatureFlagProviderNotFound()
        {
            var provider = new InMemoryFeatureFlagProvider(Array.Empty<FeatureFlag>());

            var result = provider.Get("FeatureA");

            Assert.Null(result);
        }

        [Fact]
        public void TestInMemoryFeatureFlagProviderCaseInsensitive()
        {
            var flag = new FeatureFlag { Name = "FeatureA", Enabled = true };
            var provider = new InMemoryFeatureFlagProvider(new[] { flag });

            var result = provider.Get("featurea");

            Assert.NotNull(result);
            Assert.Equal(flag.Name, result.Name);
        }
    }
}