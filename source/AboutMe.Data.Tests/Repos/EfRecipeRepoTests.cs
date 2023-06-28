// <copyright file="EfRecipeRepoTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.tests.Repos;

using AboutMe.Business.Features.Item;
using AboutMe.Business.Features.Recipe;
using AboutMe.Data.Repos;
using FluentErrors.Errors;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Tests for the <see cref="EfRecipeRepo"/>.
/// </summary>
public class EfRecipeRepoTests
{
    [Fact]
    public async Task AddRecipe_UniquelyNamed_AddsEntry()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var sut = new EfRecipeRepo(db);
        var recipe = GetRecipe("test", 1.1);

        // Act
        await sut.AddRecipe(recipe, ToItems(recipe));
        db = GetDb(connection);
        var dbRecipe = db.Recipes.Single(r => r.Name == recipe.Name);

        // Assert
        dbRecipe.Should().BeEquivalentTo(recipe);
    }

    [Fact]
    public async Task AddRecipe_NoItems_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);
        var recipe = GetRecipe("test", 1.1);

        // Act
        var act = () => sut.AddRecipe(recipe, null!);

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage("Items must be specified.");
    }

    [Fact]
    public async Task AddRecipe_SpuriousItems_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);
        var recipe = GetRecipe("test", 1.1);

        // Act
        var act = () => sut.AddRecipe(recipe, new() { ["eggz"] = 2 });

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage("Not all items were found.");
    }

    [Fact]
    public async Task AddRecipe_AlreadyExists_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);
        var dbRecipe = GetRecipe("test", 1.1);
        db.Recipes.Add(dbRecipe);
        db.SaveChanges();

        // Act
        var act = () => sut.AddRecipe(dbRecipe, ToItems(dbRecipe));

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage($"Recipe with name '{dbRecipe.Name}' already exists.");
    }

    [Fact]
    public async Task AddRecipe_RecipeNull_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);

        // Act
        var act = () => sut.AddRecipe(null!, new());

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage("Recipe must be specified.");
    }

    [Fact]
    public void SearchItems_WhenCalled_ReturnsExpected()
    {
        // Arrange
        const double portion = 2.3;
        var db = GetDb();
        var sut = new EfRecipeRepo(db);
        var dbRecipe = GetRecipe("test", portion);
        db.Recipes.Add(dbRecipe);
        db.SaveChanges();

        // Act
        var results = sut.SearchRecipes(dbRecipe.Name, 1, 1);

        // Assert
        results.Data.Single().StandardPortionKg.Should().Be(portion);
    }

    [Fact]
    public async Task UpdateRecipe_ItemFound_AltersEntry()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var sut = new EfRecipeRepo(db);
        var dbRecipe = GetRecipe("test", 1.1);
        db.Recipes.Add(dbRecipe);
        db.SaveChanges();
        var updateRecipe = GetRecipe("test", 2.2);

        // Act
        await sut.UpdateRecipe(updateRecipe, ToItems(updateRecipe));
        db = GetDb(connection);
        dbRecipe = db.Recipes.Single(r => r.Name == dbRecipe.Name);

        // Assert
        dbRecipe.Should().BeEquivalentTo(updateRecipe);
    }

    [Fact]
    public async Task UpdateRecipe_SwapIngredients_AltersEntry()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var sut = new EfRecipeRepo(db);
        var dbRecipe = GetRecipe("test", 1.1, true);
        dbRecipe.Ingredients!.Add(new() { AmountKg = 1, Item = new() { Name = "foo" } });
        db.Recipes.Add(dbRecipe);
        db.Items.Add(new() { Name = "bar" });
        db.SaveChanges();
        var updateRecipe = GetRecipe("test", 2.2, true);
        updateRecipe.Ingredients!.Add(new() { AmountKg = 2, Item = new() { Name = "bar" } });

        // Act
        await sut.UpdateRecipe(updateRecipe, ToItems(updateRecipe));
        db = GetDb(connection);
        var ingredient = db.Recipes
            .Include(r => r.Ingredients)
            .Single(r => r.Name == dbRecipe.Name)
            .Ingredients!
            .Single();

        // Assert
        ingredient.AmountKg.Should().Be(updateRecipe.Ingredients.Single().AmountKg);
    }

    [Fact]
    public async Task UpdateRecipe_ItemNotFound_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);
        const string attemptName = "test";

        // Act
        var act = () => sut.UpdateRecipe(new() { Name = attemptName }, ToItems(new()));

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage($"Recipe with name '{attemptName}' was not found.");
    }

    [Fact]
    public async Task UpdateRecipe_RecipeNull_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);

        // Act
        var act = () => sut.UpdateRecipe(null!, new());

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage("Recipe must be specified.");
    }

    [Fact]
    public async Task UpdateRecipe_EmptyItems_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);
        const string attemptName = "test";

        // Act
        var act = () => sut.UpdateRecipe(new() { Name = attemptName }, null!);

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage("Items must be specified.");
    }

    [Fact]
    public async Task DeleteRecipe_ItemFound_RemovesEntry()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var sut = new EfRecipeRepo(db);
        var dbRecipe = GetRecipe("test", 1.1);
        db.Recipes.Add(dbRecipe);
        db.SaveChanges();

        // Act
        await sut.DeleteRecipe(dbRecipe.Name);
        db = GetDb(connection);
        dbRecipe = db.Recipes.SingleOrDefault(r => r.Name == dbRecipe.Name);

        // Assert
        dbRecipe.Should().BeNull();
    }

    [Fact]
    public async Task DeleteRecipe_ItemNotFound_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfRecipeRepo(db);
        const string attemptName = "test";

        // Act
        var act = () => sut.DeleteRecipe(attemptName);

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage($"Recipe with name '{attemptName}' was not found.");
    }

    [Fact]
    public async Task DeleteRecipe_WithIngredients_RemovesIngredients()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var sut = new EfRecipeRepo(db);
        var dbRecipe = GetRecipe("test", 1.1, true);
        var dbItem = new ItemModel { Name = "bar" };
        var dbIngredient = new IngredientModel { AmountKg = 0.2, Item = dbItem, Recipe = dbRecipe };
        db.Recipes.Add(dbRecipe);
        db.Items.Add(dbItem);
        db.Ingredients.Add(dbIngredient);
        db.SaveChanges();

        // Act
        await sut.DeleteRecipe(dbRecipe.Name);
        db = GetDb(connection);
        var ingredient = db.Ingredients.SingleOrDefault();

        // Assert
        ingredient.Should().BeNull();
    }

    private static MeContext GetDb(string? connectionId = null)
    {
        var optsBuilder = new DbContextOptionsBuilder<MeContext>();
        optsBuilder.UseInMemoryDatabase(connectionId ?? Guid.NewGuid().ToString());
        return new MeContext(optsBuilder.Options);
    }

    private static RecipeModel GetRecipe(string stringVal, double doubleVal, bool newSet = false) => new()
    {
        Name = stringVal,
        StandardPortionKg = doubleVal,
        Ingredients = newSet ? new() : null,
    };

    private static Dictionary<string, double> ToItems(RecipeModel recipe)
        => recipe.Ingredients?.ToDictionary(r => r.Item.Name, r => r.AmountKg) ?? new();
}
