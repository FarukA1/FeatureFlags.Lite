using System.Net;
using FeatureFlags.Lite.AspNet.Context;
using FeatureFlags.Lite.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureFlags.Lite.AspNet.Tests
{
    public class FeatureGateContextTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public FeatureGateContextTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task FeatureD_WithAllowedRole_ReturnsOk()
        {
            var client = ClientWithRoles("Admin");

            var response = await client.GetAsync("/Demo/feature-d");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FeatureD_WithDisallowedRole_ReturnsNotFound()
        {
            var client = ClientWithRoles("Guest");

            var response = await client.GetAsync("/Demo/feature-d");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private HttpClient ClientWithRoles(params string[] roles)
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IHttpContextFeatureContextFactory>(
                        new StubFeatureContextFactory(new FeatureContext { Roles = roles }));
                });
            }).CreateClient();
        }

        private sealed class StubFeatureContextFactory : IHttpContextFeatureContextFactory
        {
            private readonly FeatureContext _context;

            public StubFeatureContextFactory(FeatureContext context) => _context = context;

            public FeatureContext Create(HttpContext httpContext) => _context;
        }
    }
}
