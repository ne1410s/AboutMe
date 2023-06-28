// <copyright file="RecipesFeatureTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.tests.FrameworkIntegration;

using System.Net.Http.Json;
using Me.Api.Features.Item;
using Me.Api.Features.Recipe;
using Me.Business.Features.Common;
using Me.Business.Features.Item;
using Me.Business.Features.Recipe;
using Me.Data;

/// <summary>
/// Tests for the endpoints defined in the <see cref="RecipesController"/> class.
/// </summary>
public class RecipesFeatureTests
{
    private readonly HttpClient client;
    private readonly MeContext db;

    public RecipesFeatureTests()
    {
        var appFactory = new TestingWebAppFactory();
        this.client = appFactory.CreateClient();
        this.db = appFactory.Db;
        this.db.Items.AddRange(
            new() { Name = "Apples" },
            new() { Name = "Oranges" });
        this.db.Recipes.AddRange(
            new() { Name = "Recipe1", StandardPortionKg = 12.3 },
            new() { Name = "Recipe2" },
            new() { Name = "Recipe3", Ingredients = this.GetIngredients(new() { ["Apples"] = 23 }) },
            new() { Name = "Recipe4" });
        this.db.SaveChanges();
    }

    [Fact]
    public async Task Add_WhenCalled_AddsEntry()
    {
        // Arrange
        const string serviceUrl = "recipes";
        var entry = new RecipeWebModel { Name = "Recipe101", StandardPortionKg = 0.66 };

        // Act
        await this.client.PostAsJsonAsync(serviceUrl, entry);
        var added = this.db.Recipes.Single(r => r.Name == entry.Name);

        // Assert
        added.StandardPortionKg.Should().Be(entry.StandardPortionKg);
    }

    [Fact]
    public async Task Search_WhenCalled_ReturnsList()
    {
        // Arrange
        const string serviceUrl = "recipes/search/Recipe1?pageSize=5";
        var expected = new RecipeWebModel { Name = "Recipe1", StandardPortionKg = 12.3 };

        // Act
        var response = await this.client.GetAsync(serviceUrl);
        var result = await response.Content.ReadFromJsonAsync<PageResult<RecipeWebModel>>();

        // Assert
        result!.Data.Should().BeEquivalentTo(new[] { expected });
    }

    [Fact]
    public async Task Search_NoName_SuppliesEmptyString()
    {
        // Arrange
        const string serviceUrl = "recipes/search/?pageSize=5";

        // Act
        var response = await this.client.GetAsync(serviceUrl);
        var result = await response.Content.ReadFromJsonAsync<PageResult<ItemWebModel>>();

        // Assert
        result!.Data.Count().Should().Be(4);
    }

    [Fact]
    public async Task Update_WhenCalled_AltersEntry()
    {
        // Arrange
        const string serviceUrl = "recipes";
        var entry = new RecipeWebModel
        {
            Name = "Recipe3",
            StandardPortionKg = 1.1,
            Ingredients = new(),
        };

        // Act
        await this.client.PutAsJsonAsync(serviceUrl, entry);
        var updated = this.db.Recipes.Single(r => r.Name == entry.Name);

        // Assert
        updated.StandardPortionKg.Should().Be(entry.StandardPortionKg);
        updated.Ingredients.Should().BeEmpty();
    }

    [Fact]
    public async Task Delete_WhenCalled_RemovesEntry()
    {
        // Arrange
        const string entryName = "Recipe4";
        const string serviceUrl = $"recipes/{entryName}";

        // Act
        await this.client.DeleteAsync(serviceUrl);
        var entryResult = this.db.Recipes.SingleOrDefault(r => r.Name == entryName);

        // Assert
        entryResult.Should().BeNull();
    }

    private HashSet<IngredientModel> GetIngredients(Dictionary<string, double> items)
    {
        return items.Keys.Select(key => new IngredientModel
        {
            Item = new ItemModel { Name = key },
            AmountKg = items[key],
        }).ToHashSet();
    }
}