// <copyright file="ItemsFeatureTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.tests.FrameworkIntegration;

using System.Net;
using System.Net.Http.Json;
using AboutMe.Api.Features.Item;
using AboutMe.Api.Features.Recipe;
using AboutMe.Business.Features.Common;
using AboutMe.Data;
using FluentErrors.Api.Models;

/// <summary>
/// Tests for the endpoints defined in the <see cref="ItemsController"/> class.
/// </summary>
public class ItemsFeatureTests
{
    private readonly HttpClient client;
    private readonly MeContext db;

    public ItemsFeatureTests()
    {
        var appFactory = new TestingWebAppFactory();
        this.client = appFactory.CreateClient();
        this.db = appFactory.Db;
        this.db.Items.AddRange(
            new() { Name = "Item1", StandardPortionKg = 0.085 },
            new() { Name = "Item2", SpecificCarbs = 0.5 },
            new() { Name = "Item3" },
            new() { Name = "Item4", SpecificFat = 0.22 });
        this.db.SaveChanges();
    }

    [Fact]
    public async Task Add_WhenCalled_AddsEntry()
    {
        // Arrange
        const string serviceUrl = "items";
        var entry = new RecipeWebModel { Name = "Item101", StandardPortionKg = 0.31 };

        // Act
        await this.client.PostAsJsonAsync(serviceUrl, entry);
        var added = this.db.Items.Single(r => r.Name == entry.Name);

        // Assert
        added.StandardPortionKg.Should().Be(entry.StandardPortionKg);
    }

    [Fact]
    public async Task Add_NameAlreadyExists_ReturnsErrorMessage()
    {
        // Arrange
        const string serviceUrl = "items";
        const string expectedError = "Item with name 'Item1' already exists.";
        var entry = new RecipeWebModel { Name = "Item1", StandardPortionKg = 0.31 };

        // Act
        var response = await this.client.PostAsJsonAsync(serviceUrl, entry);
        var error = await response.Content.ReadFromJsonAsync<HttpErrorBody>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        error!.Message.Should().Be(expectedError);
    }

    [Fact]
    public async Task Search_WhenCalled_ReturnsList()
    {
        // Arrange
        const string serviceUrl = "items/search/Item2?pageSize=5";
        var expected = new ItemWebModel("Item2", SpecificCarbs: 0.5);

        // Act
        var response = await this.client.GetAsync(serviceUrl);
        var result = await response.Content.ReadFromJsonAsync<PageResult<ItemWebModel>>();

        // Assert
        result!.Data.Should().BeEquivalentTo(new[] { expected });
    }

    [Fact]
    public async Task Search_NoName_SuppliesEmptyString()
    {
        // Arrange
        const string serviceUrl = "items/search/?pageSize=5";

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
        const string serviceUrl = "items";
        var entry = new ItemWebModel("Item3", SpecificFibre: 0.002);

        // Act
        await this.client.PutAsJsonAsync(serviceUrl, entry);
        var updated = this.db.Items.Single(r => r.Name == entry.Name);

        // Assert
        updated.SpecificFibre.Should().Be(entry.SpecificFibre);
    }

    [Fact]
    public async Task Delete_WhenCalled_RemovesEntry()
    {
        // Arrange
        const string entryName = "Item4";
        const string serviceUrl = $"items/{entryName}";

        // Act
        await this.client.DeleteAsync(serviceUrl);
        var entryResult = this.db.Items.SingleOrDefault(r => r.Name == entryName);

        // Assert
        entryResult.Should().BeNull();
    }
}
