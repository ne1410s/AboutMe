// <copyright file="CorsStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Common;

using FluentErrors.Extensions;

/// <summary>
/// Extensions for configuring cross-origin resource sharing at startup.
/// </summary>
public static class CorsStartupExtensions
{
    /// <summary>
    /// Adds the CORS feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The config.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddCorsFeature(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        configuration.MustExist();
        var corsHeaders = configuration.GetSection("Cors:Headers").Get<string[]>();
        var corsOrigins = configuration.GetSection("Dynamic:Global:CorsOrigins").Get<string[]>();
        return services.AddCors(o => o
            .AddDefaultPolicy(builder => builder
                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                .WithHeaders(corsHeaders)
                .WithOrigins(corsOrigins)
                .SetIsOriginAllowedToAllowWildcardSubdomains()));
    }

    /// <summary>
    /// Uses the CORS feature.
    /// </summary>
    /// <param name="app">The app builder.</param>
    /// <returns>The same app builder.</returns>
    public static IApplicationBuilder UseCorsFeature(
        this IApplicationBuilder app)
    {
        return app.UseCors();
    }
}
