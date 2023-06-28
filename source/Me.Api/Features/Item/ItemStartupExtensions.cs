// <copyright file="ItemStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Item;

using Me.Business.Features.Common;
using Me.Business.Features.Item;
using Me.Data.Repos;

/// <summary>
/// Extensions for configuring items at startup.
/// </summary>
public static class ItemStartupExtensions
{
    /// <summary>
    /// Adds the item feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddItemFeature(
        this IServiceCollection services) => services
            .AddScoped<IMapperBidirectional<ItemModel, ItemWebModel>, ItemMapper>()
            .AddScoped<IItemService, ItemService>()
            .AddScoped<IItemRepo, EfItemRepo>();
}
