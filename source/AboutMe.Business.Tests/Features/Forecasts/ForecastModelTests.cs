// <copyright file="ForecastModelTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Tests.Features.Forecasts;

using AboutMe.Business.Features.Forecasts;

/// <summary>
/// Tests for the <see cref="ForecastModel"/>.
/// </summary>
public class ForecastModelTests
{
    [Fact]
    public void Ctor_WhenCalled_ConvertsExpectedly()
    {
        // Arrange
        const double centigrade = 5;
        const double expectedConversionF = 41;

        // Act
        var actual = new ForecastModel(centigrade, string.Empty);

        // Assert
        actual.TemperatureF.ShouldBe(expectedConversionF);
    }
}
