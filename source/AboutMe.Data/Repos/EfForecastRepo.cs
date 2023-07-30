// <copyright file="EfForecastRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.Repos;

using System.Threading.Tasks;
using AboutMe.Business.Features.Forecasts;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IForecastRepo"/>
public class EfForecastRepo : IForecastRepo
{
    private readonly AboutMeContext db;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfForecastRepo"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    public EfForecastRepo(AboutMeContext db)
    {
        this.db = db;
    }

    /// <inheritdoc/>
    public async Task<ForecastModel?> GetItem()
    {
        return await this.db.Forecasts
            .OrderBy(r => r.TemperatureC)
            .FirstOrDefaultAsync();
    }
}
