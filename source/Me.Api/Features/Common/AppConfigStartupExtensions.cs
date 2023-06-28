// <copyright file="AppConfigStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Common;

using Azure.Identity;
using FluentErrors.Extensions;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using Me.Api.Features.Config;

/// <summary>
/// Extensions for configuring Azure App Config at startup.
/// </summary>
public static class AppConfigStartupExtensions
{
    /// <summary>
    /// Adds the Azure App Config feature.
    /// </summary>
    /// <param name="appBuilder">The host builder.</param>
    public static void AddAppConfigFeature(this WebApplicationBuilder appBuilder)
    {
        appBuilder.MustExist();
        appBuilder.WebHost.ConfigureAppConfiguration((context, configBuilder) =>
        {
            var config = configBuilder.Build();
            var connection = config.GetConnectionString("AppConfig");

            if (!string.IsNullOrWhiteSpace(connection))
            {
                var treatConnectionAsUri = connection.StartsWith("http");
                var env = context.HostingEnvironment.EnvironmentName.ToLower();

                var sp = appBuilder.Services.BuildServiceProvider();
                var logger = sp.GetRequiredService<ILogger<object>>();
                logger.LogInformation("Environment: {Env}", env);

                Func<AzureAppConfigurationOptions, AzureAppConfigurationOptions> connector = treatConnectionAsUri
                    ? opts => opts.Connect(new Uri(connection!), new DefaultAzureCredential())
                    : opts => opts.Connect(connection);

                configBuilder.AddAzureAppConfiguration(o => connector(o)
                    .UseFeatureFlags(o => o
                        .Select(KeyFilter.Any, LabelFilter.Null)
                        .Select(KeyFilter.Any, env)
                        .CacheExpirationInterval = TimeSpan.FromMinutes(5))
                    .Select("Dynamic:Nutrition:*", env)
                    .Select("Dynamic:Nutrition:*", LabelFilter.Null)
                    .Select("Dynamic:Global:*", env)
                    .Select("Dynamic:Global:*", LabelFilter.Null)
                    .ConfigureRefresh(o => o
                        .Register("Dynamic:Nutrition:Sentinel", true)
                        .Register("Dynamic:Global:Sentinel", true)
                        .SetCacheExpiration(TimeSpan.FromMinutes(5))));
            }
        });

        var config = appBuilder.Configuration;
        appBuilder.Services
            .Configure<DynamicSettings>(config.GetSection("Dynamic"))
            .Configure<FeatureFlagOptions>(config.GetSection("FeatureManagement"))
            .AddAzureAppConfiguration()
            .AddFeatureManagement();
    }

    /// <summary>
    /// Uses the Azure App Configuration feature.
    /// </summary>
    /// <param name="app">The app.</param>
    /// <param name="config">The configuration.</param>
    /// <returns>App builder.</returns>
    public static IApplicationBuilder UseAppConfigFeature(
        this WebApplication app,
        IConfiguration config)
    {
        var connection = config.GetConnectionString("AppConfig");
        if (!string.IsNullOrWhiteSpace(connection))
        {
            app.UseAzureAppConfiguration();
        }

        return app;
    }
}
