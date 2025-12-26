using FeatureFlags.Lite.AspNet.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Lite.AspNetDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        [HttpGet("feature-a")]
        [FeatureGate("FeatureA")]
        public IActionResult FeatureA() => Ok("FeatureA is enabled!");

        [HttpGet("feature-b")]
        [FeatureGate("FeatureB")]
        public IActionResult FeatureB() => Ok("FeatureB is enabled!");

        [HttpGet("feature-c")]
        [FeatureGate("FeatureC")]
        public IActionResult FeatureC() => Ok("FeatureC is enabled for your rollout percentage!");

        [HttpGet("feature-d")]
        [FeatureGate("FeatureD")]
        public IActionResult FeatureD() => Ok("FeatureD is enabled for allowed users!");

        [HttpGet("feature-e")]
        [FeatureGate("FeatureE")]
        public IActionResult FeatureE() => Ok("FeatureE is enabled for this environment!");

        [HttpGet("public")]
        public IActionResult Public() => Ok("Public endpoint, always accessible.");
    }
}