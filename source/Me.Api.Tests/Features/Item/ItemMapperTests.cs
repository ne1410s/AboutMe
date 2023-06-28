// <copyright file="ItemMapperTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.tests.Features.Item;

using Me.Api.Features.Item;
using Me.Business.Features.Item;

/// <summary>
/// Tests for the <see cref="ItemMapper"/>.
/// </summary>
public class ItemMapperTests
{
    [Fact]
    public void Map_Null_ReturnsNull()
    {
        // Arrange
        var item = (ItemModel)null!;
        var sut = new ItemMapper();

        // Act
        var result = sut.Map(item);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MapBack_Null_ReturnsNull()
    {
        // Arrange
        var item = (ItemWebModel)null!;
        var sut = new ItemMapper();

        // Act
        var result = sut.MapBack(item);

        // Assert
        result.Should().BeNull();
    }
}
