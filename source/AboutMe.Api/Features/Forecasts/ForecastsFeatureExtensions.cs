// <copyright file="ForecastsFeatureExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Forecasts;

using AboutMe.Business.Features.Forecasts;
using AboutMe.Data.Repos;

/// <summary>
/// Extensions for configuring forecasts at startup.
/// </summary>
public static class ForecastsFeatureExtensions
{
    /// <summary>
    /// Adds the forecasts feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddForecastsFeature(
        this IServiceCollection services) => services
            .AddScoped<IForecastService, ForecastService>()
            .AddScoped<IForecastRepo, EfForecastRepo>();
}
