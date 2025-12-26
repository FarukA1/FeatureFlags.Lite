using System.Net;
using FluentAssertions;

namespace FeatureFlags.Lite.AspNet.Tests
{
    public class PublicEndpointTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PublicEndpointTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Endpoint_Returns_Success()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Demo/public");

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}