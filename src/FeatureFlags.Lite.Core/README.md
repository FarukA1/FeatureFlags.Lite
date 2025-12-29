# FeatureFlags.Lite

[![License](https://img.shields.io/github/license/FarukA1/FeatureFlags.Lite)](LICENSE)

## Overview

`FeatureFlags.Lite` is a lightweight, easy-to-use feature flag library for .NET applications. It supports in-memory and configuration-based feature flags, with optional ASP.NET Core middleware for attribute-based endpoint gating.

## Key Features

- Toggle features on/off via configuration or in-memory provider.
- Rollout support for percentage-based feature flags.
- Environment-specific overrides.
- Role-based access control via `AllowedRoles`.
- ASP.NET Core middleware for `[FeatureGate("FeatureName")]` attributes.

## Installation

```bash
git clone https://github.com/FarukA1/FeatureFlags.Lite.git
```

## Quick Start

### Add services

```bash
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
```bash
app.UseFeatureFlags();
```

### Protect endpoints with `[FeatureGate]`

``` bash
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

```bash{
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

``` bash
var inMemoryProvider = new InMemoryFeatureFlagProvider(new[]
{
    new FeatureFlag { Name = "FeatureA", Enabled = true },
    new FeatureFlag { Name = "FeatureB", Enabled = false }
});

var service = new FeatureFlagService(inMemoryProvider, new DefaultFeatureEvaluator());
service.IsEnabled("FeatureA");
```

## Contribution

Contributions is welcomed. If you find a bug or have an enhancement in mind, feel free to open an issue or submit a pull request.



