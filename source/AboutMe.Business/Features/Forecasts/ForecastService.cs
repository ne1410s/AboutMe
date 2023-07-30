// <copyright file="ForecastService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Forecasts;

/// <inheritdoc cref="IForecastService"/>
public class ForecastService : IForecastService
{
    private readonly IForecastRepo forecastRepo;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForecastService"/> class.
    /// </summary>
    /// <param name="forecastRepo">The forecast repo.</param>
    public ForecastService(IForecastRepo forecastRepo)
    {
        this.forecastRepo = forecastRepo;
    }

    /// <inheritdoc/>
    public async Task<ForecastModel?> GetItem() => await this.forecastRepo.GetItem();
}
