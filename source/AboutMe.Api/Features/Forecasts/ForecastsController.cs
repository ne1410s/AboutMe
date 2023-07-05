// <copyright file="ForecastsController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Forecasts;

using AboutMe.Business.Features.Forecasts;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The forecasts controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ForecastsController : ControllerBase
{
    private readonly IForecastService forecastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForecastsController"/> class.
    /// </summary>
    /// <param name="forecastService">The forecast service.</param>
    public ForecastsController(IForecastService forecastService)
    {
        this.forecastService = forecastService;
    }

    /// <summary>
    /// Gets a forecast.
    /// </summary>
    /// <param name="empirical">Whether use old skool units.</param>
    /// <returns>A forecast.</returns>
    [HttpGet]
    public async Task<ForecastWebModel> Get(bool empirical = false)
    {
        var forecast = await this.forecastService.GetItem();
        var correctedTemp = empirical ? forecast.TemperatureF : forecast.TemperatureC;
        return new(correctedTemp, forecast.Description);
    }
}
