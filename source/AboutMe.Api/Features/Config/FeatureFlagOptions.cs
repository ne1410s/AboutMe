// <copyright file="FeatureFlagOptions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Config;

using System.ComponentModel;

/// <summary>
/// Feature flags.
/// </summary>
public class FeatureFlagOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether test feature 1 is active.
    /// </summary>
    [Description("Test feature 1.")]
    public bool TestFeature1 { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether test feature 2 is active.
    /// </summary>
    [Description("Test feature 2.")]
    public bool TestFeature2 { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether test feature 3 is active.
    /// </summary>
    public bool TestFeature3 { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether test feature 4 is active.
    /// </summary>
    public bool TestFeature4 { get; set; }
}