// <copyright file="ForecastsControllerTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Tests.Features.Forecasts;

using AboutMe.Api.Features.Forecasts;
using AboutMe.Business.Features.Forecasts;
using Moq;

/// <summary>
/// Tests for the <see cref="ForecastsController"/>.
/// </summary>
public class ForecastsControllerTests
{
    [Theory]
    [InlineData(true, 41)]
    [InlineData(false, 5)]
    public async Task Get_WhenCalled_ReturnsExpected(bool empirical, double expectedTemp)
    {
        // Arrange
        var mockResult = new ForecastModel(5, "Sunny");
        var mockService = new Mock<IForecastService>();
        _ = mockService.Setup(m => m.GetItem()).ReturnsAsync(mockResult);
        var sut = new ForecastsController(mockService.Object);
        var expected = new ForecastWebModel(expectedTemp, mockResult.Description);

        // Act
        var result = await sut.Get(empirical);

        // Assert
        result.ShouldBe(expected);
    }
}
