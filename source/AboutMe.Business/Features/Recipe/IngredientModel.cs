// <copyright file="IngredientModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Recipe;

using AboutMe.Business.Features.Item;

/// <summary>
/// An ingredient.
/// </summary>
public record IngredientModel
{
    /// <summary>
    /// Gets or sets the amount in kilograms.
    /// </summary>
    public double AmountKg { get; set; }

    /// <summary>
    /// Gets or sets the item.
    /// </summary>
    public ItemModel Item { get; set; } = null!;

    /// <summary>
    /// Gets or sets the recipe.
    /// </summary>
    public RecipeModel Recipe { get; set; } = null!;
}
