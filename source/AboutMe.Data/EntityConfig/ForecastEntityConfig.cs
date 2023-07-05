// <copyright file="ForecastEntityConfig.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.EntityConfig;

using AboutMe.Business.Features.Forecasts;
using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the <see cref="ForecastModel"/>.
/// </summary>
public class ForecastEntityConfig : IEntityTypeConfiguration<ForecastModel>
{
    private const string TableName = "Forecast";
    private const string KeyColumn = TableName + "Id";
    private const string DescriptionColumn = nameof(ForecastModel.Description);
    private const string TemperatureColumn = nameof(ForecastModel.TemperatureC);

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ForecastModel> builder)
    {
        builder.MustExist();
        builder.ToTable(TableName, b =>
        {
            b.HasCheckConstraint(
                $"CK_{TableName}_{DescriptionColumn}_AtLeastThreeChars",
                $"LEN([{DescriptionColumn}]) >= 3");

            b.HasCheckConstraint(
                $"CK_{TableName}_{DescriptionColumn}_Trimmed",
                $"LTRIM(RTRIM([{DescriptionColumn}])) = [{DescriptionColumn}]");

            b.HasCheckConstraint(
                $"CK_{TableName}_{TemperatureColumn}_GreaterThanAbsoluteZero",
                $"ISNULL([{TemperatureColumn}], 1) >= -273.15");
        });

        builder.Property<int>(KeyColumn).ValueGeneratedOnAdd();
        builder.HasKey(KeyColumn);

        builder.Property(m => m.Description).HasMaxLength(255).IsRequired();
        builder.Property(m => m.TemperatureC);
    }
}
