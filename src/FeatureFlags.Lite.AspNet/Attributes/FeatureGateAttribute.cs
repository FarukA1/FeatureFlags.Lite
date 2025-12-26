using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FeatureFlags.Lite.Core.Abstractions;

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

            if (!flags.IsEnabled(FeatureName))
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }
    }
}
