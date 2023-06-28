// <copyright file="IItemRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Item;

using AboutMe.Business.Features.Common;
using FluentErrors.Errors;

/// <summary>
/// Repository for nutrition items.
/// </summary>
public interface IItemRepo
{
    /// <summary>
    /// Adds a new item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Asynchronous task.</returns>
    /// <exception cref="DataStateException">State errors.</exception>
    public Task AddItem(ItemModel item);

    /// <summary>
    /// Updates an existing item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Asynchronous task.</returns>
    /// <exception cref="DataStateException">State errors.</exception>
    public Task UpdateItem(ItemModel item);

    /// <summary>
    /// Deletes an item by its name.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>Asynchronous task.</returns>
    public Task DeleteItem(string itemName);

    /// <summary>
    /// Searches for items.
    /// </summary>
    /// <param name="nameSearch">The name search.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>Paged results.</returns>
    public PageResult<ItemModel> SearchItems(string nameSearch, int pageNumber, int pageSize);
}
