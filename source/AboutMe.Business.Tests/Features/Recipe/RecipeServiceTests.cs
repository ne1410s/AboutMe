// <copyright file="RecipeServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.tests.Features.Recipe;

using AboutMe.Business.Features.Recipe;

/// <summary>
/// Tests for the <see cref="RecipeService"/>.
/// </summary>
public class RecipeServiceTests
{
    [Fact]
    public async Task AddItem_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IRecipeRepo>();
        var sut = new RecipeService(mockRepo.Object);
        var recipe = new RecipeModel { Name = "foo", StandardPortionKg = 1 };
        var items = new Dictionary<string, double> { ["bar"] = 12 };

        // Act
        await sut.AddRecipe(recipe, items);

        // Assert
        mockRepo.Verify(m => m.AddRecipe(recipe, items));
    }

    [Fact]
    public void SearchRecipes_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IRecipeRepo>();
        var sut = new RecipeService(mockRepo.Object);

        // Act
        sut.SearchRecipes("foo", 12, 8);

        // Assert
        mockRepo.Verify(m => m.SearchRecipes("foo", 12, 8));
    }

    [Fact]
    public async Task UpdateItem_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IRecipeRepo>();
        var sut = new RecipeService(mockRepo.Object);
        var ingredients = new[] { new IngredientModel { AmountKg = 2, Item = new() { Name = "bar" } } };
        var recipe = new RecipeModel { Name = "foo", StandardPortionKg = 1, Ingredients = ingredients.ToHashSet() };
        var expectedItems = new Dictionary<string, double> { ["bar"] = 2 };

        // Act
        await sut.UpdateRecipe(recipe);

        // Assert
        mockRepo.Verify(m => m.UpdateRecipe(recipe, expectedItems));
    }

    [Fact]
    public async Task UpdateItem_NullIngredients_SendsEmptyItems()
    {
        // Arrange
        var mockRepo = new Mock<IRecipeRepo>();
        var sut = new RecipeService(mockRepo.Object);
        var recipe = new RecipeModel { Name = "foo", StandardPortionKg = 1, Ingredients = null };

        // Act
        await sut.UpdateRecipe(recipe);

        // Assert
        mockRepo.Verify(m => m.UpdateRecipe(recipe, new()));
    }

    [Fact]
    public async Task DeleteItem_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IRecipeRepo>();
        var sut = new RecipeService(mockRepo.Object);

        // Act
        await sut.DeleteRecipe("foo");

        // Assert
        mockRepo.Verify(m => m.DeleteRecipe("foo"));
    }
}
