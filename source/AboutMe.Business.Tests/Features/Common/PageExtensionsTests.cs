// <copyright file="PageExtensionsTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.tests.Features.Common;

using AboutMe.Business.Features.Common;

/// <summary>
/// Tests for the <see cref="PageExtensions"/>.
/// </summary>
public class PageExtensionsTests
{
    [Fact]
    public void Page_ZeroValuedParams_SetToOne()
    {
        // Arrange
        var sequence = new[] { 1, 2, 3 };

        // Act
        var result = sequence.Page(0, 0);

        // Assert
        result.PageNumber.ShouldBe(1);
        result.PageSize.ShouldBe(1);
    }

    [Fact]
    public void Page_SkipAndTake_YieldsExpected()
    {
        // Arrange
        var sequence = new[] { 1, 2, 3, 4, 5 };
        int[] expected = [3, 4];

        // Act
        var result = sequence.Page(2, 2);

        // Assert
        result.Data.ToArray().ShouldBeEquivalentTo(expected);
        result.TotalPages.ShouldBe(3);
    }

    [Fact]
    public void MapTo_IsNull_ReturnsNull()
    {
        // Arrange
        var input = (PageResult<int>)null!;

        // Act
        var result = input.MapTo(i => i);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void MapTo_NotNull_MapsValues()
    {
        // Arrange
        var expected = new double[] { 1, 2, 3 };
        var input = new PageResult<int>()
        {
            Data = [1, 2, 3],
            PageNumber = 2,
            PageSize = 3,
            TotalPages = 4,
            TotalRecords = 12,
        };

        // Act
        var result = input.MapTo(i => (double)i);

        // Assert
        result.Data.ToArray().ShouldBeEquivalentTo(expected);
    }
}
