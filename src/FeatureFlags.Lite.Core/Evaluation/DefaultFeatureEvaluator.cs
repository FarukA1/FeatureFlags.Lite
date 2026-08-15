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

                var hash = StableHash(context.UserId) % 100;
                return hash < flag.RolloutPercentage;
            }

            return true;
        }

        private static int StableHash(string value)
        {
            unchecked
            {
                const uint fnvPrime = 16777619;
                var hash = 2166136261;
                foreach (var b in System.Text.Encoding.UTF8.GetBytes(value))
                {
                    hash ^= b;
                    hash *= fnvPrime;
                }
                return (int)(hash & 0x7FFFFFFF);
            }
        }
    }
}
