namespace FeatureFlags.Lite.Core.Models
{
    public sealed class FeatureContext
    {
        public string? UserId { get; init; }
        public IReadOnlyCollection<string> Roles { get; init; } = Array.Empty<string>();
        public string? Environment { get; init; }

        public static FeatureContext Empty => new();
    }
}
