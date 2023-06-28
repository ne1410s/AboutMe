// <copyright file="RecipeModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.Features.Recipe;

/// <summary>
/// A recipe.
/// </summary>
public record RecipeModel
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the standard portion, in kilograms.
    /// </summary>
    public double? StandardPortionKg { get; set; }

    /// <summary>
    /// Gets the ingredients list.
    /// </summary>
    public HashSet<IngredientModel>? Ingredients { get; init; }
}