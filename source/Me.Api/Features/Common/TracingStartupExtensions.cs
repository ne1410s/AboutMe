// <copyright file="TracingStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Common;

using FluentErrors.Extensions;

/// <summary>
/// Extensions for configuring tracing at startup.
/// </summary>
public static class TracingStartupExtensions
{
    /// <summary>
    /// Adds the tracing feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="config">The config.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddTracingFeature(
        this IServiceCollection services,
        IConfiguration config)
    {
        config.MustExist();
        var connection = config["Dynamic:Global:Connections:AppInsights"];
        return services.AddApplicationInsightsTelemetry(o =>
        {
            if (!string.IsNullOrWhiteSpace(connection))
            {
                o.ConnectionString = connection;
            }
        });
    }
}
