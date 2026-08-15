using System.Security.Claims;
using FeatureFlags.Lite.AspNet.Context;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace FeatureFlags.Lite.AspNet.Tests
{
    public class HttpContextFeatureContextFactoryTests
    {
        [Fact]
        public void Create_PrefersAuthenticatedClaim_OverSpoofableHeader()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-User-Id"] = "attacker-supplied";
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.NameIdentifier, "authenticated-user") },
                "TestAuth"));

            var factory = new HttpContextFeatureContextFactory(new FakeHostEnvironment());

            var context = factory.Create(httpContext);

            context.UserId.Should().Be("authenticated-user");
        }

        [Fact]
        public void Create_FallsBackToHeader_WhenNoAuthenticatedClaim()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-User-Id"] = "anonymous-caller";

            var factory = new HttpContextFeatureContextFactory(new FakeHostEnvironment());

            var context = factory.Create(httpContext);

            context.UserId.Should().Be("anonymous-caller");
        }

        [Fact]
        public void Create_SourcesEnvironment_FromHostEnvironment_NotClientHeader()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Environment"] = "Production";

            var factory = new HttpContextFeatureContextFactory(new FakeHostEnvironment { EnvironmentName = "Staging" });

            var context = factory.Create(httpContext);

            context.Environment.Should().Be("Staging");
        }

        private sealed class FakeHostEnvironment : IHostEnvironment
        {
            public string EnvironmentName { get; set; } = "Development";
            public string ApplicationName { get; set; } = "Test";
            public string ContentRootPath { get; set; } = ".";
            public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        }
    }
}
