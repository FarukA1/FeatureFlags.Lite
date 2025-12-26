using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FeatureFlags.Lite.AspNet.Extensions;
using FeatureFlags.Lite.Core.Abstractions;

namespace FeatureFlags.Lite.AspNet.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var settings = new Dictionary<string, string>
                {
                    ["FeatureFlags:FeatureA:Enabled"] = "true",
                    ["FeatureFlags:FeatureB:Enabled"] = "false"
                };

                config.AddInMemoryCollection(settings);
            });

            builder.ConfigureServices(services =>
            {
                services.AddFeatureFlags();

                services.AddSingleton<IFeatureFlagProvider, ConfigurationFeatureFlagProvider>();
            });
        }
    }
}