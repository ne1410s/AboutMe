// <copyright file="RecipeWebModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Recipe;

/// <summary>
/// A recipe.
/// </summary>
public record RecipeWebModel
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Gets the standard portion, in kilograms.
    /// </summary>
    public double? StandardPortionKg { get; init; }

    /// <summary>
    /// Gets the ingredients.
    /// </summary>
    public HashSet<IngredientWebModel> Ingredients { get; init; } = new();
}