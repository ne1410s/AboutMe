// <copyright file="DateTimeServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using Me.Business.Features.Common;

namespace Me.Business.tests.Features.Common;

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
        var result = sut.OffsetNow();

        // Assert
        result.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
    }
}
