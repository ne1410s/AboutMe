// <copyright file="RecipeEntityConfig.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.EntityConfig;

using AboutMe.Business.Features.Recipe;
using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the <see cref="RecipeModel"/>.
/// </summary>
public class RecipeEntityConfig : IEntityTypeConfiguration<RecipeModel>
{
    private const string TableName = "Recipe";
    private const string KeyColumn = TableName + "Id";
    private const string NameColumn = nameof(RecipeModel.Name);
    private const string PortionColumn = nameof(RecipeModel.StandardPortionKg);

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RecipeModel> builder)
    {
        builder.MustExist();
        builder.ToTable(TableName, b =>
        {
            b.HasCheckConstraint(
                $"CK_{TableName}_{NameColumn}_AtLeastThreeChars",
                $"LEN([{NameColumn}]) >= 3");

            b.HasCheckConstraint(
                $"CK_{TableName}_{NameColumn}_Trimmed",
                $"LTRIM(RTRIM([{NameColumn}])) = [{NameColumn}]");

            b.HasCheckConstraint(
                $"CK_{TableName}_{PortionColumn}_GreaterThanZero",
                $"ISNULL([{PortionColumn}], 1) > 0");
        });

        builder.Property<int>(KeyColumn).ValueGeneratedOnAdd();
        builder.HasKey(KeyColumn);

        builder.HasIndex(m => m.Name)
            .HasDatabaseName($"UQ_{TableName}_{NameColumn}")
            .IsUnique();

        builder.Property(m => m.Name).HasMaxLength(255).IsRequired();
        builder.Property(m => m.StandardPortionKg);
    }
}
