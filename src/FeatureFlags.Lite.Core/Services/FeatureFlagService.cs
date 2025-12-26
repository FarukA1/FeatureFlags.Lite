using FeatureFlags.Lite.Core.Abstractions;
using FeatureFlags.Lite.Core.Models;

public sealed class FeatureFlagService : IFeatureFlagService
{
    private readonly IFeatureFlagProvider _provider;
    private readonly IFeatureEvaluator _evaluator;

    public FeatureFlagService(IFeatureFlagProvider provider, IFeatureEvaluator evaluator)
    {
        _provider = provider;
        _evaluator = evaluator;
    }

    public bool IsEnabled(string featureName, FeatureContext? context = null)
    {
        var flag = _provider.Get(featureName);
        if (flag == null)
            return false;

        return _evaluator.Evaluate(flag, context ?? FeatureContext.Empty);
    }
}
