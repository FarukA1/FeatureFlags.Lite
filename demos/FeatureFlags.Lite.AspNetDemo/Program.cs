using FeatureFlags.Lite.AspNet.Extensions;
using FeatureFlags.Lite.Core.Abstractions;
using FeatureFlags.Lite.Core.Providers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFeatureFlags();

builder.Services.AddSingleton<ConfigurationFeatureFlagProvider>(sp =>
{
    var configuration = builder.Configuration;
    return new ConfigurationFeatureFlagProvider(configuration);
});

builder.Services.AddSingleton<IFeatureFlagProvider>(sp =>
    sp.GetRequiredService<ConfigurationFeatureFlagProvider>()
);

var app = builder.Build();

app.UseFeatureFlags();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


public partial class Program { }