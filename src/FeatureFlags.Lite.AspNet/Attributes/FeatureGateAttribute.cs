using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FeatureFlags.Lite.Core.Abstractions;
using FeatureFlags.Lite.Core.Models;
using FeatureFlags.Lite.AspNet.Context;

namespace FeatureFlags.Lite.AspNet.Attributes
{
    public sealed class FeatureGateAttribute : Attribute, IAsyncActionFilter
    {
        public string FeatureName { get; }

        public FeatureGateAttribute(string featureName)
        {
            FeatureName = featureName;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var flags = context.HttpContext.RequestServices.GetRequiredService<IFeatureFlagService>();
            var contextFactory = context.HttpContext.RequestServices.GetService<IHttpContextFeatureContextFactory>();

            var featureContext = contextFactory?.Create(context.HttpContext) ?? FeatureContext.Empty;

            if (!flags.IsEnabled(FeatureName, featureContext))
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }
    }
}
