// <copyright file="DateTimeServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.tests.Features.Common;

using AboutMe.Business.Features.Common;

/// <summary>
/// Tests for the <see cref="DateTimeService"/>.
/// </summary>
public class DateTimeServiceTests
{
    [Fact]
    public void OffsetNow_WhenCalled_ReturnsOffset()
    {
        // Arrange
        var sut = new DateTimeService();

        // Act
        var result = sut.OffsetNow().ToUnixTimeSeconds();
        var expected = DateTimeOffset.Now.ToUnixTimeSeconds();

        // Assert
        result.ShouldBeInRange(expected - 5, expected + 5);
    }
}
