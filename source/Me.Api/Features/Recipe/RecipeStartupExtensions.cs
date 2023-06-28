﻿// <copyright file="RecipeStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Recipe;

using Me.Business.Features.Common;
using Me.Business.Features.Recipe;
using Me.Data.Repos;

/// <summary>
/// Extensions for configuring recipes at startup.
/// </summary>
public static class RecipeStartupExtensions
{
    /// <summary>
    /// Adds the recipe feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddRecipeFeature(
        this IServiceCollection services) => services
            .AddScoped<IMapperBidirectional<IngredientModel, IngredientWebModel>, IngredientMapper>()
            .AddScoped<IMapperBidirectional<RecipeModel, RecipeWebModel>, RecipeMapper>()
            .AddScoped<IRecipeService, RecipeService>()
            .AddScoped<IRecipeRepo, EfRecipeRepo>();
}
