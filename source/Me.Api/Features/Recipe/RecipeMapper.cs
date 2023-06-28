// <copyright file="RecipeMapper.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Recipe;

using Me.Business.Features.Common;
using Me.Business.Features.Recipe;

/// <inheritdoc cref="IMapperBidirectional{TSource, TTarget}"/>
public class RecipeMapper : IMapperBidirectional<RecipeModel, RecipeWebModel>
{
    private readonly IMapperBidirectional<IngredientModel, IngredientWebModel> ingredientMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecipeMapper"/> class.
    /// </summary>
    public RecipeMapper()
    {
        this.ingredientMapper = new IngredientMapper();
    }

    /// <inheritdoc/>
    public RecipeWebModel Map(RecipeModel source) => source == null ? null! : new()
    {
        Name = source.Name,
        StandardPortionKg = source.StandardPortionKg,
        Ingredients = source.Ingredients?.Select(this.ingredientMapper.Map).ToHashSet() ?? new(),
    };

    /// <inheritdoc/>
    public RecipeModel MapBack(RecipeWebModel target) => target == null ? null! : new()
    {
        Name = target.Name,
        StandardPortionKg = target.StandardPortionKg,
        Ingredients = target.Ingredients?.Select(this.ingredientMapper.MapBack).ToHashSet(),
    };
}
