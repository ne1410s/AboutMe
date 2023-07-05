// <copyright file="ForecastModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Forecasts;

/// <summary>
/// A forecast.
/// </summary>
/// <param name="TemperatureC">The temperature in degrees Centigrade.</param>
/// <param name="Description">The description.</param>
public record ForecastModel(double TemperatureC, string Description)
{
    /// <summary>
    /// Gets the temperature in degrees Fahrenheit.
    /// </summary>
    public double TemperatureF => 32 + (this.TemperatureC * 9 / 5);
}
