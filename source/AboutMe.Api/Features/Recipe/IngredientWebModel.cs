﻿// <copyright file="IngredientWebModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Recipe;

using AboutMe.Api.Features.Item;

/// <summary>
/// An ingredient.
/// </summary>
public record IngredientWebModel(
    double AmountKg,
    ItemWebModel Item);