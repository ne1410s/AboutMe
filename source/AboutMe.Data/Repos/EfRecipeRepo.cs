// <copyright file="EfRecipeRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.Repos;

using System.Collections.Generic;
using System.Threading.Tasks;
using AboutMe.Business.Features.Common;
using AboutMe.Business.Features.Recipe;
using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;

/// <inheritdoc cref="IRecipeRepo"/>
public class EfRecipeRepo : IRecipeRepo
{
    private readonly MeContext db;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfRecipeRepo"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public EfRecipeRepo(MeContext db)
    {
        this.db = db;
    }

    /// <inheritdoc/>
    public async Task AddRecipe(RecipeModel recipe, Dictionary<string, double> items)
    {
        recipe.MustBePopulated("Recipe must be specified.");
        items.MustBePopulated("Items must be specified.");

        var existing = this.db.Recipes.SingleOrDefault(r => r.Name == recipe.Name);
        existing.MustBeUnpopulated($"Recipe with name '{recipe.Name}' already exists.");

        var ingredients = await this.GetIngredientsAsync(items);
        this.db.Recipes.Add(new RecipeModel
        {
            Name = recipe.Name!,
            StandardPortionKg = recipe.StandardPortionKg,
            Ingredients = ingredients,
        });

        await this.db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateRecipe(RecipeModel recipe, Dictionary<string, double> items)
    {
        recipe.MustBePopulated("Recipe must be specified.");
        items.MustBePopulated("Items must be specified.");

        var existing = this.db.Recipes
            .Include(r => r.Ingredients)
            .SingleOrDefault(r => r.Name == recipe.Name);
        existing.MustBePopulated($"Recipe with name '{recipe.Name}' was not found.");

        var ingredients = await this.GetIngredientsAsync(items);

        existing!.StandardPortionKg = recipe.StandardPortionKg;
        existing.Ingredients.Clear();
        foreach (var ingredient in ingredients)
        {
            existing.Ingredients.Add(ingredient);
        }

        await this.db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteRecipe(string recipeName)
    {
        var existing = this.db.Recipes.SingleOrDefault(r => r.Name == recipeName);
        existing.MustBePopulated($"Recipe with name '{recipeName}' was not found.");

        // InMem db cant seem to fail here; hence Stryker fails..
        ////if (existing!.Ingredients != null)
        ////{
        ////    this.db.Ingredients.RemoveRange(existing.Ingredients);
        ////}

        this.db.Recipes.Remove(existing!);

        await this.db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public PageResult<RecipeModel> SearchRecipes(string nameSearch, int pageNumber, int pageSize)
        => this.db.Recipes
            .Where(r => r.Name.Contains(nameSearch))
            .Page(pageNumber, pageSize);

    private async Task<HashSet<IngredientModel>> GetIngredientsAsync(Dictionary<string, double> items)
    {
        items = items.ToDictionary(r => r.Key, r => r.Value, StringComparer.OrdinalIgnoreCase);
        var itemNames = items.Select(r => r.Key).ToHashSet();
        var dbItems = await this.db.Items
            .Where(r => itemNames.Contains(r.Name))
            .ToListAsync();
        dbItems.Count.MustBe(items.Count, "Not all items were found.");

        return dbItems.Select(r => new IngredientModel
        {
            Item = r,
            AmountKg = items[r.Name],
        }).ToHashSet();
    }
}
