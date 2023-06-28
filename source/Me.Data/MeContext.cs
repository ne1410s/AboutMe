// <copyright file="MeContext.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Data;

using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;
using Me.Business.Features.Item;
using Me.Business.Features.Recipe;
using Me.Data.EntityConfig;

/// <summary>
/// Nutrition database.
/// </summary>
public class MeContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeContext"/> class.
    /// </summary>
    /// <param name="options">The db context options.</param>
    public MeContext(DbContextOptions<MeContext> options)
        : base(options)
    { }

    /// <summary>
    /// Gets or sets the nutrition items.
    /// </summary>
    public virtual DbSet<ItemModel> Items { get; set; } = default!;

    /// <summary>
    /// Gets or sets the recipes.
    /// </summary>
    public virtual DbSet<RecipeModel> Recipes { get; set; } = default!;

    /// <summary>
    /// Gets or sets the ingredients.
    /// </summary>
    public virtual DbSet<IngredientModel> Ingredients { get; set; } = default!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.MustExist();
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ItemEntityConfig());
        modelBuilder.ApplyConfiguration(new IngredientEntityConfig());
        modelBuilder.ApplyConfiguration(new RecipeEntityConfig());
    }
}
