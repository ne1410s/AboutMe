// <copyright file="HealthCheckStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Common;

/// <summary>
/// Extensions for adding health checks at startup.
/// </summary>
public static class HealthCheckStartupExtensions
{
    /// <summary>
    /// Adds healthchecks feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddHealthChecksFeature(
        this IServiceCollection services)
    {
        services.AddHealthChecks();

        return services;
    }

    /// <summary>
    /// Uses healthchecks feature.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public static void UseHealthChecksFeature(
        this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/startup", new()
        {
            // startup: no other checks until this passes
            Predicate = _ => false,
        });
        app.MapHealthChecks("/ready", new()
        {
            // readiness: initialized, core deps only
            Predicate = c => c.Tags.Contains("ready"),
        });
        app.MapHealthChecks("/health", new()
        {
            // liveness: everything is awesome
            Predicate = _ => true,
        });
    }
}