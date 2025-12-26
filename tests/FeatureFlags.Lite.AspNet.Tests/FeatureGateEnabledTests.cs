using System.Net;
using FluentAssertions;

namespace FeatureFlags.Lite.AspNet.Tests
{
    public class FeatureGateEnabledTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public FeatureGateEnabledTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task TestFeatureA_WhenEnabled_ReturnsOk()
        {
            var response = await _client.GetAsync("/Demo/feature-a");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestFeatureB_WhenDisabled_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/Demo/feature-b");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
