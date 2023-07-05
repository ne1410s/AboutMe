// <copyright file="EfForecastRepoTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.Tests.Repos;

using AboutMe.Business.Features.Forecasts;
using AboutMe.Data.Repos;

/// <summary>
/// Tests for the <see cref="EfForecastRepo"/>.
/// </summary>
public class EfForecastRepoTests
{
    [Fact]
    public async Task GetItem_WhenCalled_ReturnsExpected()
    {
        // Arrange
        var db = TestHelper.GetDb();
        var expected = new ForecastModel(5, "Cold");
        db.Forecasts.Add(expected);
        db.SaveChanges();
        var sut = new EfForecastRepo(db);

        // Act
        var result = await sut.GetItem();

        // Assert
        result.Should().Be(expected);
    }
}
