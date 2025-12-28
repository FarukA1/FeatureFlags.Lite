namespace FeatureFlags.Lite.Core.Models
{
    public sealed class FeatureFlag
    {
        public string Name { get; init; } = default!;
        public bool Enabled { get; init; }

        public int? RolloutPercentage { get; init; }
        public IReadOnlyCollection<string>? AllowedRoles { get; init; }
        public IReadOnlyDictionary<string, bool>? EnvironmentOverrides { get; init; }
    }
}
