// <copyright file="ConfigController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Config;

using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

/// <summary>
/// Config controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ConfigController
{
    private readonly DynamicSettings settings;
    private readonly FeatureFlagOptions featureFlagOptions;
    private readonly IFeatureManagerSnapshot featureManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigController"/> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <param name="featureFlags">The feature flags.</param>
    /// <param name="featureManager">The feature manager.</param>
    public ConfigController(
        IOptionsSnapshot<DynamicSettings> settings,
        IOptionsSnapshot<FeatureFlagOptions> featureFlags,
        IFeatureManagerSnapshot featureManager)
    {
        this.settings = settings?.Value!;
        this.featureFlagOptions = featureFlags?.Value!;
        this.featureManager = featureManager;
    }

    /// <summary>
    /// Gets settings.
    /// </summary>
    /// <returns>The settings.</returns>
    [HttpGet]
    [Route("settings")]
    public DynamicSettings GetSettings() => this.settings;

    /// <summary>
    /// Gets feature flag information.
    /// </summary>
    /// <returns>Feature flag information.</returns>
    [HttpGet]
    [Route("features")]
    public Dictionary<string, FeatureWebModel> GetFeatures()
    {
        return this.featureFlagOptions == null ? null! : typeof(FeatureFlagOptions)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
            .ToDictionary(prop => prop.Name, prop => new FeatureWebModel(
                prop.Name,
                prop.GetCustomAttribute<DescriptionAttribute>()?.Description,
                (bool)prop.GetValue(this.featureFlagOptions)!));
    }

    /// <summary>
    /// Gets whether a feature is enabled or not.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>Whether enabled.</returns>
    [HttpGet]
    [Route("features/{name}")]
    public async Task<bool> IsFeatureEnabled(string name) =>
        await this.featureManager.IsEnabledAsync(name);
}
