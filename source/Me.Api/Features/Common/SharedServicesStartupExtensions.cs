// <copyright file="SharedServicesStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Common;

using Me.Business.Features.Common;

/// <summary>
/// Extensions for adding shared services at startup.
/// </summary>
public static class SharedServicesStartupExtensions
{
    /// <summary>
    /// Adds shared services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services) => services
            .AddScoped<IDateTimeService, DateTimeService>();
}
