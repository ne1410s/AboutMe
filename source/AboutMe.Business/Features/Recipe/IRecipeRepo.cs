﻿// <copyright file="IRecipeRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Recipe;

using AboutMe.Business.Features.Common;
using FluentErrors.Errors;

/// <summary>
/// Repository for recipes.
/// </summary>
public interface IRecipeRepo
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
    /// Updates an existing recipe, based on existing items.
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    /// <param name="items">The item names and amounts.</param>
    /// <returns>Asynchronous task.</returns>
    /// <exception cref="DataStateException">State errors.</exception>
    public Task UpdateRecipe(RecipeModel recipe, Dictionary<string, double> items);

    /// <summary>
    /// Deletes a recipe by its name.
    /// </summary>
    /// <param name="recipeName">The recipe name.</param>
    /// <returns>Asynchronous task.</returns>
    /// <exception cref="DataStateException">State errors.</exception>
    public Task DeleteRecipe(string recipeName);

    /// <summary>
    /// Searches for recipes.
    /// </summary>
    /// <param name="nameSearch">The name search.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>Paged results.</returns>
    public PageResult<RecipeModel> SearchRecipes(string nameSearch, int pageNumber, int pageSize);
}