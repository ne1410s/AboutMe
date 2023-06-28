// <copyright file="ItemService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Item;

using AboutMe.Business.Features.Common;

/// <inheritdoc cref="IItemService"/>
public class ItemService : IItemService
{
    private readonly IItemRepo itemRepo;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemService"/> class.
    /// </summary>
    /// <param name="itemRepo">The item repo.</param>
    public ItemService(IItemRepo itemRepo)
    {
        this.itemRepo = itemRepo;
    }

    /// <inheritdoc/>
    public async Task AddItem(ItemModel item)
        => await this.itemRepo.AddItem(item);

    /// <inheritdoc/>
    public PageResult<ItemModel> SearchItems(string nameSearch, int pageNumber, int pageSize)
        => this.itemRepo.SearchItems(nameSearch, pageNumber, pageSize);

    /// <inheritdoc/>
    public async Task UpdateItem(ItemModel item)
        => await this.itemRepo.UpdateItem(item);

    /// <inheritdoc/>
    public async Task DeleteItem(string itemName)
        => await this.itemRepo.DeleteItem(itemName);
}
