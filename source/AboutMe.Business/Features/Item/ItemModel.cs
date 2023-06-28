// <copyright file="ItemModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Item;

using AboutMe.Business.Features.Recipe;

/// <summary>
/// A nutrition item.
/// </summary>
public record ItemModel
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the standard portion, in kg.
    /// </summary>
    public double? StandardPortionKg { get; set; }

    /// <summary>
    /// Gets or sets the calories per kilogram.
    /// </summary>
    public double? KCalsPerKg { get; set; }

    /// <summary>
    /// Gets or sets the mass-based proportion of carbohydrates.
    /// </summary>
    public double? SpecificCarbs { get; set; }

    /// <summary>
    /// Gets or sets the mass-based proportion of carbohydrates, of which sugars.
    /// </summary>
    public double? SpecificSugars { get; set; }

    /// <summary>
    /// Gets or sets the mass-based proportion of fat.
    /// </summary>
    public double? SpecificFat { get; set; }

    /// <summary>
    /// Gets or sets the mass-based proportion of fat, of which saturates.
    /// </summary>
    public double? SpecificSaturates { get; set; }

    /// <summary>
    /// Gets or sets the mass-based proportion of protein.
    /// </summary>
    public double? SpecificProtein { get; set; }

    /// <summary>
    /// Gets or sets the mass-based proportion of fibre.
    /// </summary>
    public double? SpecificFibre { get; set; }

    /// <summary>
    /// Gets or sets the mass-based proportion of sodium.
    /// </summary>
    public double? SpecificSodium { get; set; }

    /// <summary>
    /// Gets the ingredients list.
    /// </summary>
    public HashSet<IngredientModel>? Ingredients { get; init; }
}
