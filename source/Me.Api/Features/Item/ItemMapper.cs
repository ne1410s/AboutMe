// <copyright file="ItemMapper.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Item;

using Me.Business.Features.Common;
using Me.Business.Features.Item;

/// <inheritdoc cref="IMapperBidirectional{TSource, TTarget}"/>
public class ItemMapper : IMapperBidirectional<ItemModel, ItemWebModel>
{
    /// <inheritdoc/>
    public ItemWebModel Map(ItemModel source) => source == null ? null! : new(
        source.Name,
        source.StandardPortionKg,
        source.KCalsPerKg,
        source.SpecificCarbs,
        source.SpecificSugars,
        source.SpecificFat,
        source.SpecificSaturates,
        source.SpecificProtein,
        source.SpecificFibre,
        source.SpecificSodium);

    /// <inheritdoc/>
    public ItemModel MapBack(ItemWebModel target) => target == null ? null! : new()
    {
        Name = target.Name,
        StandardPortionKg = target.StandardPortionKg,
        KCalsPerKg = target.KCalsPerKg,
        SpecificCarbs = target.SpecificCarbs,
        SpecificSugars = target.SpecificSugars,
        SpecificFat = target.SpecificFat,
        SpecificSaturates = target.SpecificSaturates,
        SpecificProtein = target.SpecificProtein,
        SpecificFibre = target.SpecificFibre,
        SpecificSodium = target.SpecificSodium,
    };
}
