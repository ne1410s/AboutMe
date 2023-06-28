// <copyright file="RecipeMapperTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.tests.Features.Recipe;

using Me.Api.Features.Recipe;
using Me.Business.Features.Recipe;

/// <summary>
/// Tests for the <see cref="RecipeMapper"/>.
/// </summary>
public class RecipeMapperTests
{
    [Fact]
    public void Map_GivenNull_ReturnsNull()
    {
        // Arrange
        var sut = new RecipeMapper();
        var input = (RecipeModel)null!;

        // Act
        var result = sut.Map(input!);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Map_GivenIngredients_IncludesInMap()
    {
        // Arrange
        var sut = new RecipeMapper();
        var expected = new IngredientWebModel(12, new("Banana"));
        var input = new RecipeModel
        {
            Name = "Smoothie",
            StandardPortionKg = 0.5,
            Ingredients = new IngredientModel[]
            {
               new()
               {
                   AmountKg = expected.AmountKg,
                   Item = new() { Name = expected.Item.Name },
               },
            }.ToHashSet(),
        };

        // Act
        var result = sut.Map(input);

        // Assert
        result.Ingredients.Should().BeEquivalentTo(new[] { expected });
    }

    [Fact]
    public void MapBack_GivenNull_ReturnsNull()
    {
        // Arrange
        var sut = new RecipeMapper();
        var input = (RecipeWebModel)null!;

        // Act
        var result = sut.MapBack(input!);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MapBack_GivenValue_ReturnsExpected()
    {
        // Arrange
        var sut = new RecipeMapper();
        var input = new RecipeWebModel { Name = "Test", StandardPortionKg = 2, Ingredients = new() };
        var expected = new RecipeModel
        {
            Name = input.Name,
            StandardPortionKg = input.StandardPortionKg,
            Ingredients = new(),
        };

        // Act
        var result = sut.MapBack(input);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void MapBack_GivenValueNullIngredients_ReturnsExpected()
    {
        // Arrange
        var sut = new RecipeMapper();
        var input = new RecipeWebModel { Name = "Test", StandardPortionKg = 2, Ingredients = null! };
        var expected = new RecipeModel
        {
            Name = input.Name,
            StandardPortionKg = input.StandardPortionKg,
        };

        // Act
        var result = sut.MapBack(input);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
