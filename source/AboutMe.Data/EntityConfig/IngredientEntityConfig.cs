// <copyright file="IngredientEntityConfig.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.EntityConfig;

using AboutMe.Business.Features.Recipe;
using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the <see cref="IngredientModel"/>.
/// </summary>
public class IngredientEntityConfig : IEntityTypeConfiguration<IngredientModel>
{
    private const string TableName = "Ingredient";
    private const string KeyColumn = TableName + "Id";
    private const string ItemFKColumn = "ItemId";
    private const string RecipeFKColumn = "RecipeId";
    private const string AmountColumn = nameof(IngredientModel.AmountKg);

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<IngredientModel> builder)
    {
        builder.MustExist();
        builder.ToTable(TableName, b =>
        {
            b.HasCheckConstraint(
                $"CK_{TableName}_{AmountColumn}_GreaterThanZero",
                $"[{AmountColumn}] > 0");
        });

        builder.Property<int>(KeyColumn).ValueGeneratedOnAdd();
        builder.HasKey(KeyColumn);

        builder.Property<int>(ItemFKColumn);
        builder.Property<int>(RecipeFKColumn);
        builder.HasIndex(ItemFKColumn, RecipeFKColumn)
            .HasDatabaseName($"UQ_{TableName}_{ItemFKColumn}_{RecipeFKColumn}")
            .IsUnique();

        builder.HasOne(m => m.Item).WithMany(o => o.Ingredients).HasForeignKey(ItemFKColumn);
        builder.HasOne(m => m.Recipe).WithMany(o => o.Ingredients).HasForeignKey(RecipeFKColumn);

        builder.Property(m => m.AmountKg);
    }
}
