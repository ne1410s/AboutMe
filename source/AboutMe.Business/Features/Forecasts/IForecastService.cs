// <copyright file="IForecastService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Forecasts;

/// <summary>
/// Forecast service.
/// </summary>
public interface IForecastService
{
    /// <summary>
    /// Gets a forecast.
    /// </summary>
    /// <returns>The forecast.</returns>
    public Task<ForecastModel> GetItem();
}
