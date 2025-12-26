using System.Security.Claims;
using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.AspNet.Context
{
    public interface IHttpContextFeatureContextFactory
    {
        FeatureContext Create(HttpContext httpContext);
    }

    public class HttpContextFeatureContextFactory : IHttpContextFeatureContextFactory
    {
        public FeatureContext Create(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            
            var userId = httpContext.Request.Headers["X-User-Id"].FirstOrDefault() 
                         ?? httpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var environment = httpContext.Request.Headers["X-Environment"].FirstOrDefault();

            var roles = httpContext.User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() 
                        ?? new List<string>();

            return new FeatureContext
            {
                UserId = userId,
                Environment = environment,
                Roles = roles
            };
        }
    }
}