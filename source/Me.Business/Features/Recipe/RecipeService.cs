// <copyright file="RecipeService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.Features.Recipe;

using Me.Business.Features.Common;

/// <inheritdoc cref="IRecipeService"/>
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepo recipeRepo;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecipeService"/> class.
    /// </summary>
    /// <param name="recipeRepo">The recipe repo.</param>
    public RecipeService(IRecipeRepo recipeRepo)
    {
        this.recipeRepo = recipeRepo;
    }

    /// <inheritdoc/>
    public async Task AddRecipe(RecipeModel recipe, Dictionary<string, double> items)
        => await this.recipeRepo.AddRecipe(recipe, items);

    /// <inheritdoc/>
    public PageResult<RecipeModel> SearchRecipes(string nameSearch, int pageNumber, int pageSize)
        => this.recipeRepo.SearchRecipes(nameSearch, pageNumber, pageSize);

    /// <inheritdoc/>
    public async Task UpdateRecipe(RecipeModel recipe)
    {
        var items = recipe.Ingredients?.ToDictionary(i => i.Item.Name, i => i.AmountKg);
        await this.recipeRepo.UpdateRecipe(recipe, items ?? new());
    }

    /// <inheritdoc/>
    public async Task DeleteRecipe(string recipeName)
        => await this.recipeRepo.DeleteRecipe(recipeName);
}
