// <copyright file="IngredientMapperTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.tests.Features.Recipe;

using Me.Api.Features.Item;
using Me.Api.Features.Recipe;
using Me.Business.Features.Recipe;

/// <summary>
/// Tests for the <see cref="IngredientMapper"/>.
/// </summary>
public class IngredientMapperTests
{
    [Fact]
    public void Map_GivenNull_ReturnsNull()
    {
        // Arrange
        var sut = new IngredientMapper();
        var input = (IngredientModel)null!;

        // Act
        var result = sut.Map(input!);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MapBack_GivenNull_ReturnsNull()
    {
        // Arrange
        var sut = new IngredientMapper();
        var input = (IngredientWebModel)null!;

        // Act
        var result = sut.MapBack(input!);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MapBack_GivenValue_ReturnsExpected()
    {
        // Arrange
        var sut = new IngredientMapper();
        var input = new IngredientWebModel(12, new ItemWebModel("Test"));
        var expected = new IngredientModel { AmountKg = input.AmountKg, Item = new() { Name = input.Item.Name } };

        // Act
        var result = sut.MapBack(input);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
