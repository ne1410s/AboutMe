// <copyright file="ForecastsControllerTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Tests.Features.Forecasts;

using AboutMe.Api.Features.Forecasts;

/// <summary>
/// Tests for the <see cref="ForecastsController"/>.
/// </summary>
public class ForecastsControllerTests
{
    [Theory]
    [InlineData(true, 90)]
    [InlineData(false, 30)]
    public void Get_WhenCalled_ReturnsExpected(bool empirical, double expectedTemp)
    {
        // Arrange
        var sut = new ForecastsController();
        var expected = new ForecastWebModel(expectedTemp, "Sunny");

        // Act
        var result = sut.Get(empirical);

        // Assert
        result.Should().Be(expected);
    }
}
