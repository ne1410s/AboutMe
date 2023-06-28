// <copyright file="HealthCheckStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Common;

using FluentErrors.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

/// <summary>
/// Extensions for adding health checks at startup.
/// </summary>
public static class HealthCheckStartupExtensions
{
    /// <summary>
    /// Adds healthchecks feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="config">The configuration.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddHealthChecksFeature(
        this IServiceCollection services,
        IConfiguration config)
    {
        var sqlConnection = config?["Dynamic:Global:Connections:SqlDb"];

        services
            .AddHealthChecks()
            .AddSqlServer(sqlConnection!, name: "sql", tags: new[] { "ready" });

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

/// <summary>
/// Health check for sql.
/// </summary>
public class SqlHealthCheck : IHealthCheck
{
    /// <inheritdoc/>
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        context.MustExist();
        var isHealthy = new Random().Next() % 2 == 0;

        // ...
        if (isHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}