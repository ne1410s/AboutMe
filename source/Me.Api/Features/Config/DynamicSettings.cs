// <copyright file="DynamicSettings.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Config;

/// <summary>
/// Dynamic settings.
/// </summary>
public class DynamicSettings
{
    /// <summary>
    /// Gets or sets the dynamic global settings.
    /// </summary>
    public DynamicGlobalSettings Global { get; set; } = new();

    /// <summary>
    /// Gets or sets the dynamic local settings.
    /// </summary>
    public DynamicLocalSettings Nutrition { get; set; } = new();
}

/// <summary>
/// Dynamic global settings.
/// </summary>
public class DynamicGlobalSettings
{
    /// <summary>
    /// Gets or sets the global test setting 1.
    /// </summary>
    public string? GlobalTestSetting1 { get; set; }
}

/// <summary>
/// Dynamic settings, specific to just this app.
/// </summary>
public class DynamicLocalSettings
{
    /// <summary>
    /// Gets or sets the nutrition test setting 1.
    /// </summary>
    public string? LocalTestSetting1 { get; set; }
}
