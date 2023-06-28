// <copyright file="ItemServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.tests.Features.Recipe;

using Me.Business.Features.Item;

/// <summary>
/// Tests for the <see cref="ItemService"/>.
/// </summary>
public class ItemServiceTests
{
    [Fact]
    public async Task AddItem_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IItemRepo>();
        var sut = new ItemService(mockRepo.Object);
        var item = new ItemModel { KCalsPerKg = 1, Name = "foo" };

        // Act
        await sut.AddItem(item);

        // Assert
        mockRepo.Verify(m => m.AddItem(item));
    }

    [Fact]
    public void SearchItems_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IItemRepo>();
        var sut = new ItemService(mockRepo.Object);

        // Act
        sut.SearchItems("foo", 12, 8);

        // Assert
        mockRepo.Verify(m => m.SearchItems("foo", 12, 8));
    }

    [Fact]
    public async Task UpdateItem_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IItemRepo>();
        var sut = new ItemService(mockRepo.Object);
        var item = new ItemModel { KCalsPerKg = 1, Name = "foo" };

        // Act
        await sut.UpdateItem(item);

        // Assert
        mockRepo.Verify(m => m.UpdateItem(item));
    }

    [Fact]
    public async Task DeleteItem_WhenCalled_CallsRepo()
    {
        // Arrange
        var mockRepo = new Mock<IItemRepo>();
        var sut = new ItemService(mockRepo.Object);

        // Act
        await sut.DeleteItem("foo");

        // Assert
        mockRepo.Verify(m => m.DeleteItem("foo"));
    }
}
