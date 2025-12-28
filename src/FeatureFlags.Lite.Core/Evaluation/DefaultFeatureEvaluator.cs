using FeatureFlags.Lite.Core.Abstractions;
using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.Core.Evaluation
{
    public sealed class DefaultFeatureEvaluator : IFeatureEvaluator
    {
        public bool Evaluate(FeatureFlag flag, FeatureContext context)
        {
            if (context.Environment != null && flag.EnvironmentOverrides?.TryGetValue(context.Environment, out var envEnabled) == true)
            {
                return envEnabled;
            }

            if (!flag.Enabled) return false;

            if (flag.AllowedRoles?.Any() == true)
            {
                return context.Roles.Intersect(flag.AllowedRoles).Any();
            }

            if (flag.RolloutPercentage.HasValue)
            {
                if (context.UserId == null) return false;

                var hash = Math.Abs(context.UserId.GetHashCode()) % 100;
                return hash < flag.RolloutPercentage;
            }

            return true;
        }
    }
}
