// <copyright file="EfItemRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.Repos;

using System.Threading.Tasks;
using AboutMe.Business.Features.Common;
using AboutMe.Business.Features.Item;
using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IItemRepo"/>
public class EfItemRepo : IItemRepo
{
    private readonly MeContext db;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfItemRepo"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public EfItemRepo(MeContext db)
    {
        this.db = db;
    }

    /// <inheritdoc/>
    public async Task AddItem(ItemModel item)
    {
        var existing = await this.db.Items.SingleOrDefaultAsync(r => r.Name == item.Name);
        existing.MustBeUnpopulated($"Item with name '{item.Name}' already exists.");

        item.Ingredients?.Clear();
        this.db.Items.Add(item);
        await this.db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateItem(ItemModel item)
    {
        var existing = await this.db.Items.SingleOrDefaultAsync(r => item != null && r.Name == item.Name);
        existing.MustBePopulated($"Item with name '{item?.Name}' was not found.");

        existing.StandardPortionKg = item.StandardPortionKg;
        existing.KCalsPerKg = item.KCalsPerKg;
        existing.SpecificCarbs = item.SpecificCarbs;
        existing.SpecificSugars = item.SpecificSugars;
        existing.SpecificFat = item.SpecificFat;
        existing.SpecificSaturates = item.SpecificSaturates;
        existing.SpecificProtein = item.SpecificProtein;
        existing.SpecificFibre = item.SpecificFibre;
        existing.SpecificSodium = item.SpecificSodium;
        await this.db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteItem(string itemName)
    {
        var existing = await this.db.Items.SingleOrDefaultAsync(r => r.Name == itemName);
        existing.MustBePopulated($"Item with name '{itemName}' was not found.");

        this.db.Items.Remove(existing!);
        await this.db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public PageResult<ItemModel> SearchItems(string nameSearch, int pageNumber, int pageSize)
        => this.db.Items
            .Where(r => r.Name.Contains(nameSearch))
            .Page(pageNumber, pageSize);
}
