// <copyright file="ItemsController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Item;

using AboutMe.Business.Features.Common;
using AboutMe.Business.Features.Item;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The items controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService itemService;
    private readonly IMapperBidirectional<ItemModel, ItemWebModel> itemMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsController"/> class.
    /// </summary>
    /// <param name="itemService">The item service.</param>
    /// <param name="itemMapper">The item mapper.</param>
    public ItemsController(
        IItemService itemService,
        IMapperBidirectional<ItemModel, ItemWebModel> itemMapper)
    {
        this.itemService = itemService;
        this.itemMapper = itemMapper;
    }

    /// <summary>
    /// Adds a new item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Asynchronous task.</returns>
    [HttpPost]
    public async Task Add(ItemWebModel item)
    {
        var mapped = this.itemMapper.MapBack(item);
        await this.itemService.AddItem(mapped);
    }

    /// <summary>
    /// Searches items.
    /// </summary>
    /// <param name="nameSearch">The name search.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A paged list of items.</returns>
    [HttpGet]
    [Route("search/{nameSearch?}")]
    public PageResult<ItemWebModel> Search(
        [FromRoute] string? nameSearch,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize)
    {
        return this.itemService
            .SearchItems(nameSearch ?? string.Empty, pageNumber, pageSize)
            .MapTo(this.itemMapper.Map);
    }

    /// <summary>
    /// Updates an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Asynchronous task.</returns>
    [HttpPut]
    public async Task Update(ItemWebModel item)
    {
        var mapped = this.itemMapper.MapBack(item);
        await this.itemService.UpdateItem(mapped);
    }

    /// <summary>
    /// Deletes an item.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>Asynchronous task.</returns>
    [HttpDelete]
    [Route("{itemName}")]
    public async Task Delete([FromRoute] string itemName)
    {
        await this.itemService.DeleteItem(itemName);
    }
}
