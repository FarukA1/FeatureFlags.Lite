using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.Core.Tests
{
    public class FeatureContextTests
    {
        [Fact]
        public void FeatureContextTestsDefault()
        {
            var context = FeatureContext.Empty;

            Assert.Null(context.UserId);
            Assert.Null(context.Environment);
            Assert.Empty(context.Roles);
        }

        [Fact]
        public void FeatureContextTestsWithValues()
        {
            var context = new FeatureContext
            {
                UserId = "user-123",
                Environment = "staging",
                Roles = new[] { "admin", "user" }
            };

            Assert.Equal("user-123", context.UserId);
            Assert.Equal("staging", context.Environment);
            Assert.Contains("admin", context.Roles);
            Assert.Contains("user", context.Roles);
        }

        [Fact]
        public void FeatureContextTestsWithEmptyValues()
        {
            var context = new FeatureContext
            {
                UserId = null,
                Environment = null,
                Roles = Array.Empty<string>()
            };

            Assert.Null(context.UserId);
            Assert.Null(context.Environment);
            Assert.Empty(context.Roles);
        }
    }
}