// <copyright file="ForecastsController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Forecasts;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The forecasts controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ForecastsController : ControllerBase
{
    /// <summary>
    /// Gets a forecast.
    /// </summary>
    /// <returns>A forecast.</returns>
    [HttpGet]
    public ForecastWebModel Get(bool empirical = false)
    {
        var temperature = empirical ? 90 : 30;
        return new(temperature, "Sunny");
    }
}
