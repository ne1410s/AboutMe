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
    public async Task<ForecastWebModel> Get()
    {
        await Task.CompletedTask;
        return new(30.2, "Sunny");
    }
}
