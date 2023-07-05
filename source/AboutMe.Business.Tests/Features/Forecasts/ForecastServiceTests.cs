// <copyright file="ForecastServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Tests.Features.Forecasts;

using AboutMe.Business.Features.Forecasts;
using Moq;

/// <summary>
/// Tests for the <see cref="ForecastService"/> class.
/// </summary>
public class ForecastServiceTests
{
    [Fact]
    public async Task GetItem_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IForecastRepo>();
        var sut = new ForecastService(mockRepo.Object);

        // Act
        await sut.GetItem();

        // Assert
        mockRepo.Verify(m => m.GetItem());
    }
}
