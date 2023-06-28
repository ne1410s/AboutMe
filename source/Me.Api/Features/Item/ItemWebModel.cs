// <copyright file="ItemWebModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Item;

/// <summary>
/// A nutrition item.
/// </summary>
public record ItemWebModel(
    string Name,
    double? StandardPortionKg = null,
    double? KCalsPerKg = null,
    double? SpecificCarbs = null,
    double? SpecificSugars = null,
    double? SpecificFat = null,
    double? SpecificSaturates = null,
    double? SpecificProtein = null,
    double? SpecificFibre = null,
    double? SpecificSodium = null);
