// <copyright file="DatabaseStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Common;

using Me.Data;

/// <summary>
/// Extensions for configuring database at startup.
/// </summary>
public static class DatabaseStartupExtensions
{
    /// <summary>
    /// Adds database feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="config">The config.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddDatabaseFeature(
        this IServiceCollection services,
        IConfiguration config)
    {
        var connection = config?["Dynamic:Global:Connections:SqlDb"];
        return services.AddSqlServer<MeContext>(connection);
    }
}
