// <copyright file="ItemStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Item;

using AboutMe.Business.Features.Common;
using AboutMe.Business.Features.Item;
using AboutMe.Data.Repos;

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
