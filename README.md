# FeatureFlags.Lite

[![FeatureFlags.Lite.Core](https://img.shields.io/nuget/v/FeatureFlags.Lite.Core?label=FeatureFlags.Lite.Core)](https://www.nuget.org/packages/FeatureFlags.Lite.Core)
[![FeatureFlags.Lite.AspNet](https://img.shields.io/nuget/v/FeatureFlags.Lite.AspNet?label=FeatureFlags.Lite.AspNet)](https://www.nuget.org/packages/FeatureFlags.Lite.AspNet)
[![License](https://img.shields.io/github/license/FarukA1/FeatureFlags.Lite)](LICENSE)

`FeatureFlags.Lite` is a lightweight feature flag library for .NET applications. It supports in-memory and configuration-based feature flags, with optional ASP.NET Core middleware and an attribute for endpoint gating.

## Why

[`Microsoft.FeatureManagement`](https://github.com/microsoft/FeatureManagement-Dotnet) is the dominant, actively-maintained .NET feature flag library — targeting filters, an `IFeatureFilter` pipeline, and an optional Azure App Configuration backend. `FeatureFlags.Lite` isn't trying to compete with that. It's the deliberately simpler alternative: no filter pipeline to learn, no coupling to Azure App Configuration — just config-based or in-memory flags with a `[FeatureGate]` attribute. If Microsoft's model feels heavier than a small app or solo project needs, this is the smaller option.

## Key Features

- Toggle features on/off via configuration or in-memory provider.
- Rollout support for percentage-based feature flags, with a stable per-user hash so a user's bucket doesn't change across restarts.
- Environment-specific overrides, resolved from the host environment.
- Role-based access control via `AllowedRoles`.
- ASP.NET Core middleware and a `[FeatureGate("FeatureName")]` attribute for endpoint gating.

## Installation

```bash
dotnet add package FeatureFlags.Lite.Core
dotnet add package FeatureFlags.Lite.AspNet
```

## Quick Start

### Add services

```csharp
using FeatureFlags.Lite.Core.Extensions;
using FeatureFlags.Lite.AspNet.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFeatureFlags();

// Optional: add configuration provider
builder.Services.AddSingleton<ConfigurationFeatureFlagProvider>(sp =>
    new ConfigurationFeatureFlagProvider(builder.Configuration)
);
builder.Services.AddSingleton<IFeatureFlagProvider>(sp =>
    sp.GetRequiredService<ConfigurationFeatureFlagProvider>()
);

var app = builder.Build();
```

### Use middleware in ASP.NET Core
```csharp
app.UseFeatureFlags();
```

### Protect endpoints with `[FeatureGate]`

```csharp
using FeatureFlags.Lite.AspNet.Attributes;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    [HttpGet("feature-a")]
    [FeatureGate("FeatureA")]
    public IActionResult FeatureA() => Ok("FeatureA is enabled!");

    [HttpGet("public")]
    public IActionResult Public() => Ok("Always accessible.");
}
```

### Configure feature flags via appsettings.json

```json
{
  "FeatureFlags": {
    "FeatureA": {
      "Enabled": true
    },
    "FeatureB": {
      "Enabled": false
    },
    "FeatureC": {
      "Enabled": true,
      "RolloutPercentage": 50
    },
    "FeatureD": {
      "Enabled": true,
      "AllowedRoles": [ "Admin", "Manager" ]
    },
    "FeatureE": {
      "Enabled": true,
      "EnvironmentOverrides": {
        "Development": true,
        "Production": false
      }
    }
  }
}
```
### Using in-memory feature flags (optional)

```csharp
var inMemoryProvider = new InMemoryFeatureFlagProvider(new[]
{
    new FeatureFlag { Name = "FeatureA", Enabled = true },
    new FeatureFlag { Name = "FeatureB", Enabled = false }
});

var service = new FeatureFlagService(inMemoryProvider, new DefaultFeatureEvaluator());
service.IsEnabled("FeatureA");
```

## Demo

[`demos/FeatureFlags.Lite.AspNetDemo`](demos/FeatureFlags.Lite.AspNetDemo) is a runnable ASP.NET Core API showing both packages end to end.

## Contribution

Contributions are welcome. If you find a bug or have an enhancement in mind, feel free to open an issue or submit a pull request.

## License

MIT
