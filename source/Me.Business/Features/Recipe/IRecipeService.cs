// <copyright file="IRecipeService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.Features.Recipe;

using FluentErrors.Errors;
using Me.Business.Features.Common;

/// <summary>
/// Recipe service.
/// </summary>
public interface IRecipeService
{
    /// <summary>
    /// Adds a new recipe, based on existing items.
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    /// <param name="items">The item names and amounts.</param>
    /// <returns>Asynchronous task.</returns>
    /// <exception cref="DataStateException">State errors.</exception>
    public Task AddRecipe(RecipeModel recipe, Dictionary<string, double> items);

    /// <summary>
    /// Searches for recipes.
    /// </summary>
    /// <param name="nameSearch">The name search.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>Paged results.</returns>
    public PageResult<RecipeModel> SearchRecipes(string nameSearch, int pageNumber, int pageSize);

    /// <summary>
    /// Updates a recipe.
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    /// <returns>Asynchronous task.</returns>
    public Task UpdateRecipe(RecipeModel recipe);

    /// <summary>
    /// Deletes a recipe, based on its name.
    /// </summary>
    /// <param name="recipeName">The recipe name.</param>
    /// <returns>Asynchronous task.</returns>
    /// <exception cref="DataStateException">State errors.</exception>
    public Task DeleteRecipe(string recipeName);
}
