﻿// <copyright file="ForecastWebModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Forecasts;

/// <summary>
/// A forecast.
/// </summary>
/// <param name="Temperature">The temperature.</param>
/// <param name="Description">The description.</param>
public record ForecastWebModel(double Temperature, string Description);
