using System.Security.Claims;
using FeatureFlags.Lite.Core.Models;
using Microsoft.Extensions.Hosting;

namespace FeatureFlags.Lite.AspNet.Context
{
    public interface IHttpContextFeatureContextFactory
    {
        FeatureContext Create(HttpContext httpContext);
    }

    public class HttpContextFeatureContextFactory : IHttpContextFeatureContextFactory
    {
        private readonly IHostEnvironment _hostEnvironment;

        public HttpContextFeatureContextFactory(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public FeatureContext Create(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var userId = httpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? httpContext.Request.Headers["X-User-Id"].FirstOrDefault();

            var roles = httpContext.User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
                        ?? new List<string>();

            return new FeatureContext
            {
                UserId = userId,
                Environment = _hostEnvironment.EnvironmentName,
                Roles = roles
            };
        }
    }
}