// <copyright file="EfItemRepoTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Data.tests.Repos;

using FluentErrors.Errors;
using Microsoft.EntityFrameworkCore;
using Me.Business.Features.Item;
using Me.Business.Features.Recipe;
using Me.Data.Repos;

/// <summary>
/// Tests for the <see cref="EfItemRepo"/>.
/// </summary>
public class EfItemRepoTests
{
    [Fact]
    public async Task AddItem_UniquelyNamed_SavesItem()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var item = GetItem("test", 2.2);
        var sut = new EfItemRepo(db);

        // Act
        await sut.AddItem(item);
        db = GetDb(connection);
        var dbItem = db.Items.Single(r => r.Name == item.Name);

        // Assert
        dbItem.Should().BeEquivalentTo(item);
    }

    [Fact]
    public async Task AddItem_ItemExists_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var dbItem = GetItem("test", 2.2);
        db.Items.Add(dbItem);
        db.SaveChanges();
        var sut = new EfItemRepo(db);

        // Act
        var act = () => sut.AddItem(new() { Name = dbItem.Name });

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage($"Item with name '{dbItem.Name}' already exists.");
    }

    [Fact]
    public async Task AddItem_WithIngredients_IgnoresIngredients()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var recipe = new RecipeModel() { Name = "test" };
        db.Recipes.Add(recipe);
        db.SaveChanges();
        var sut = new EfItemRepo(db);
        var item = GetItem("test", 2.2, true);
        item.Ingredients!.Add(new() { AmountKg = 1, Recipe = recipe });

        // Act
        await sut.AddItem(item);
        db = GetDb(connection);
        var dbItem = db.Items
            .Include(r => r.Ingredients)
            .Single(r => r.Name == item.Name);

        // Assert
        dbItem.Ingredients.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateItem_WhenCalled_SavesItem()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var dbItem = GetItem("test", 2.2);
        db.Items.Add(dbItem);
        db.SaveChanges();
        var sut = new EfItemRepo(db);
        var updateItem = GetItem("test", 3.3);

        // Act
        await sut.UpdateItem(updateItem);
        db = GetDb(connection);
        dbItem = db.Items.Single(r => r.Name == dbItem.Name);

        // Assert
        dbItem.Should().BeEquivalentTo(updateItem);
    }

    [Fact]
    public async Task UpdateItem_NotFound_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfItemRepo(db);
        const string attemptName = "test";

        // Act
        var act = () => sut.UpdateItem(new() { Name = attemptName });

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage($"Item with name '{attemptName}' was not found.");
    }

    [Fact]
    public async Task UpdateItem_NullPassed_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfItemRepo(db);

        // Act
        var act = () => sut.UpdateItem(null!);

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage("Item with name '' was not found.");
    }

    [Fact]
    public async Task DeleteItem_WhenCalled_RemovesItem()
    {
        // Arrange
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var dbItem = GetItem("test", 2.2);
        db.Items.Add(dbItem);
        db.SaveChanges();
        var sut = new EfItemRepo(db);

        // Act
        await sut.DeleteItem(dbItem.Name);
        db = GetDb(connection);
        dbItem = db.Items.SingleOrDefault(r => r.Name == dbItem.Name);

        // Assert
        dbItem.Should().BeNull();
    }

    [Fact]
    public async Task DeleteItem_NotFound_ThrowsException()
    {
        // Arrange
        var db = GetDb();
        var sut = new EfItemRepo(db);
        const string attemptName = "test";

        // Act
        var act = () => sut.DeleteItem(attemptName);

        // Assert
        await act.Should().ThrowAsync<DataStateException>()
            .WithMessage($"Item with name '{attemptName}' was not found.");
    }

    [Fact]
    public void SearchItems_WhenCalled_ReturnsExpected()
    {
        // Arrange
        const double specificCals = 2.2;
        var connection = Guid.NewGuid().ToString();
        var db = GetDb(connection);
        var dbItem = GetItem("test", specificCals);
        db.Items.Add(dbItem);
        db.SaveChanges();
        var sut = new EfItemRepo(db);

        // Act
        var results = sut.SearchItems(dbItem.Name, 1, 1);

        // Assert
        results.Data.Single().KCalsPerKg.Should().Be(specificCals);
    }

    private static MeContext GetDb(string? connectionId = null)
    {
        var optsBuilder = new DbContextOptionsBuilder<MeContext>();
        optsBuilder.UseInMemoryDatabase(connectionId ?? Guid.NewGuid().ToString());
        return new MeContext(optsBuilder.Options);
    }

    private static ItemModel GetItem(string stringVal, double doubleVal, bool newSet = false) => new()
    {
        Name = stringVal,
        KCalsPerKg = doubleVal,
        SpecificCarbs = doubleVal,
        SpecificFat = doubleVal,
        SpecificFibre = doubleVal,
        SpecificProtein = doubleVal,
        SpecificSaturates = doubleVal,
        SpecificSodium = doubleVal,
        SpecificSugars = doubleVal,
        StandardPortionKg = doubleVal,
        Ingredients = newSet ? new() : null,
    };
}
