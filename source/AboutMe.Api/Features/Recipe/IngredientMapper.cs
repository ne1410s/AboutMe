// <copyright file="IngredientMapper.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Recipe;

using AboutMe.Api.Features.Item;
using AboutMe.Business.Features.Common;
using AboutMe.Business.Features.Item;
using AboutMe.Business.Features.Recipe;

/// <inheritdoc cref="IMapperBidirectional{TSource, TTarget}"/>
public class IngredientMapper : IMapperBidirectional<IngredientModel, IngredientWebModel>
{
    private readonly IMapperBidirectional<ItemModel, ItemWebModel> itemMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="IngredientMapper"/> class.
    /// </summary>
    public IngredientMapper()
    {
        this.itemMapper = new ItemMapper();
    }

    /// <inheritdoc/>
    public IngredientWebModel Map(IngredientModel source) => source == null ? null! : new(
        source.AmountKg,
        this.itemMapper.Map(source.Item));

    /// <inheritdoc/>
    public IngredientModel MapBack(IngredientWebModel target) => target == null ? null! : new()
    {
        AmountKg = target.AmountKg,
        Item = this.itemMapper.MapBack(target.Item),
    };
}
