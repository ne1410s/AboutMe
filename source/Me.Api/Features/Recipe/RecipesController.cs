// <copyright file="RecipesController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Recipe;

using Microsoft.AspNetCore.Mvc;
using Me.Business.Features.Common;
using Me.Business.Features.Recipe;

/// <summary>
/// The recipes controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService recipeService;
    private readonly IMapperBidirectional<RecipeModel, RecipeWebModel> recipeMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecipesController"/> class.
    /// </summary>
    /// <param name="recipeService">The recipe service.</param>
    /// <param name="recipeMapper">The recipe mapper.</param>
    public RecipesController(
        IRecipeService recipeService,
        IMapperBidirectional<RecipeModel, RecipeWebModel> recipeMapper)
    {
        this.recipeService = recipeService;
        this.recipeMapper = recipeMapper;
    }

    /// <summary>
    /// Adds a new recipe.
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    /// <returns>Asynchronous task.</returns>
    [HttpPost]
    public async Task Add(RecipeWebModel recipe)
    {
        var mapped = this.recipeMapper.MapBack(recipe);
        var items = recipe.Ingredients!.ToDictionary(i => i.Item.Name, i => i.AmountKg);
        await this.recipeService.AddRecipe(mapped, items);
    }

    /// <summary>
    /// Searches recipes.
    /// </summary>
    /// <param name="nameSearch">The name search.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A paged list of recipes.</returns>
    [HttpGet]
    [Route("search/{nameSearch?}")]
    public PageResult<RecipeWebModel> Search(
        [FromRoute] string? nameSearch,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize)
    {
        return this.recipeService
            .SearchRecipes(nameSearch ?? string.Empty, pageNumber, pageSize)
            .MapTo(this.recipeMapper.Map);
    }

    /// <summary>
    /// Updates a recipe.
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    /// <returns>Asynchronous task.</returns>
    [HttpPut]
    public async Task Update(RecipeWebModel recipe)
    {
        var mapped = this.recipeMapper.MapBack(recipe);
        await this.recipeService.UpdateRecipe(mapped);
    }

    /// <summary>
    /// Deletes a recipe.
    /// </summary>
    /// <param name="recipeName">The recipe name.</param>
    /// <returns>Asynchronous task.</returns>
    [HttpDelete]
    [Route("{recipeName}")]
    public async Task Delete([FromRoute] string recipeName)
    {
        await this.recipeService.DeleteRecipe(recipeName);
    }
}
