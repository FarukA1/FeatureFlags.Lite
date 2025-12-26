using FeatureFlags.Lite.AspNet.Attributes;
using FeatureFlags.Lite.AspNet.Context;

namespace FeatureFlags.Lite.AspNet.Middleware
{
    public class FeatureFlagsMiddleware
    {
        private readonly RequestDelegate _next;

        public FeatureFlagsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint is null)
            {
                await _next(context);
                return;
            }

            var featureGate = endpoint.Metadata.GetMetadata<FeatureGateAttribute>();
            if (featureGate is null)
            {
                await _next(context);
                return;
            }

            var featureFlags = context.RequestServices.GetService<FeatureFlagService>();
            var featureContextFactory = context.RequestServices.GetService<IHttpContextFeatureContextFactory>();

            if (featureFlags is null || featureContextFactory is null)
            {
                await _next(context);
                return;
            }

            var featureContext = featureContextFactory.Create(context);

            if (!featureFlags.IsEnabled(featureGate.FeatureName, featureContext))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            await _next(context);
        }
    }
}