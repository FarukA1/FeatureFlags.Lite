using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.Core.Abstractions
{
   public interface IFeatureEvaluator
    {
        bool Evaluate(FeatureFlag flag, FeatureContext context);
    }
}
