// <copyright file="ItemEntityConfig.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.EntityConfig;

using AboutMe.Business.Features.Item;
using FluentErrors.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the <see cref="ItemModel"/>.
/// </summary>
public class ItemEntityConfig : IEntityTypeConfiguration<ItemModel>
{
    private const string TableName = "Item";
    private const string KeyColumn = TableName + "Id";
    private const string NameColumn = nameof(ItemModel.Name);
    private const string PortionColumn = nameof(ItemModel.StandardPortionKg);
    private const string KCalsColumn = nameof(ItemModel.KCalsPerKg);
    private const string CarbsColumn = nameof(ItemModel.SpecificCarbs);
    private const string SugarsColumn = nameof(ItemModel.SpecificSugars);
    private const string FatColumn = nameof(ItemModel.SpecificFat);
    private const string SaturatesColumn = nameof(ItemModel.SpecificSaturates);
    private const string ProteinColumn = nameof(ItemModel.SpecificProtein);
    private const string FibreColumn = nameof(ItemModel.SpecificFibre);
    private const string SodiumColumn = nameof(ItemModel.SpecificSodium);

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ItemModel> builder)
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

            b.HasCheckConstraint(
                $"CK_{TableName}_{KCalsColumn}_GreaterThanZero",
                $"ISNULL([{KCalsColumn}], 1) > 0");

            b.HasCheckConstraint(
                $"CK_{TableName}_{CarbsColumn}_BetweenZeroAndOne",
                $"[{CarbsColumn}] IS NULL OR ([{CarbsColumn}] >= 0 AND [{CarbsColumn}] <= 1)");

            b.HasCheckConstraint(
                $"CK_{TableName}_{SugarsColumn}_BetweenZeroAndOne",
                $"[{SugarsColumn}] IS NULL OR ([{SugarsColumn}] >= 0 AND [{SugarsColumn}] <= 1)");

            b.HasCheckConstraint(
                $"CK_{TableName}_{FatColumn}_BetweenZeroAndOne",
                $"[{FatColumn}] IS NULL OR ([{FatColumn}] >= 0 AND [{FatColumn}] <= 1)");

            b.HasCheckConstraint(
                $"CK_{TableName}_{SaturatesColumn}_BetweenZeroAndOne",
                $"[{SaturatesColumn}] IS NULL OR ([{SaturatesColumn}] >= 0 AND [{SaturatesColumn}] <= 1)");

            b.HasCheckConstraint(
                $"CK_{TableName}_{ProteinColumn}_BetweenZeroAndOne",
                $"[{ProteinColumn}] IS NULL OR ([{ProteinColumn}] >= 0 AND [{ProteinColumn}] <= 1)");

            b.HasCheckConstraint(
                $"CK_{TableName}_{FibreColumn}_BetweenZeroAndOne",
                $"[{FibreColumn}] IS NULL OR ([{FibreColumn}] >= 0 AND [{FibreColumn}] <= 1)");

            b.HasCheckConstraint(
                $"CK_{TableName}_{SodiumColumn}_BetweenZeroAndOne",
                $"[{SodiumColumn}] IS NULL OR ([{SodiumColumn}] >= 0 AND [{SodiumColumn}] <= 1)");
        });

        builder.Property<int>(KeyColumn).ValueGeneratedOnAdd();
        builder.HasKey(KeyColumn);

        builder.HasIndex(m => m.Name)
            .HasDatabaseName($"UQ_{TableName}_{NameColumn}")
            .IsUnique();

        builder.Property(m => m.Name).HasMaxLength(255).IsRequired();
        builder.Property(m => m.StandardPortionKg);
        builder.Property(m => m.KCalsPerKg);
        builder.Property(m => m.SpecificCarbs);
        builder.Property(m => m.SpecificSugars);
        builder.Property(m => m.SpecificFat);
        builder.Property(m => m.SpecificSaturates);
        builder.Property(m => m.SpecificProtein);
        builder.Property(m => m.SpecificFibre);
        builder.Property(m => m.SpecificSodium);
    }
}
