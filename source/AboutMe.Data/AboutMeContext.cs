// <copyright file="AboutMeContext.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data;

using AboutMe.Business.Features.Forecasts;
using AboutMe.Data.EntityConfig;
using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// AboutMe database context.
/// </summary>
public class AboutMeContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AboutMeContext"/> class.
    /// </summary>
    /// <param name="options">The db context options.</param>
    public AboutMeContext(DbContextOptions<AboutMeContext> options)
        : base(options)
    { }

    /// <summary>
    /// Gets or sets the nutrition items.
    /// </summary>
    public virtual DbSet<ForecastModel> Forecasts { get; set; } = default!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.MustExist();
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ForecastEntityConfig());
    }
}
