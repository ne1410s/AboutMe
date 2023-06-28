// <copyright file="FluentErrorStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Common;

using FluentErrors.Api;
using FluentErrors.Errors;
using FluentErrors.Extensions;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Extensions for configuring FluentErrors at startup.
/// </summary>
public static class FluentErrorStartupExtensions
{
    /// <summary>
    /// Adds FluentErrors.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    /// <exception cref="StaticValidationException">Model error.</exception>
    public static IServiceCollection AddFluentErrorsFeature(
        this IServiceCollection services)
    {
        return services.Configure<ApiBehaviorOptions>(opts =>
        {
            opts.InvalidModelStateResponseFactory = ctx =>
            {
                var invalidItems = ctx.ModelState.ToItems();
                throw new StaticValidationException(invalidItems);
            };
        });
    }

    /// <summary>
    /// Uses the FluentErrors feature.
    /// </summary>
    /// <param name="app">The app builder.</param>
    /// <returns>The same app builder.</returns>
    public static IApplicationBuilder UseFluentErrorsFeature(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<FluentErrorHandlingMiddleware>();
    }
}

/// <summary>
/// Middleware for handling of FluentErrors.
/// </summary>
public class FluentErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="FluentErrorHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The request delegate.</param>
    public FluentErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <returns>Asynchronous task.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        context.MustExist();

        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            var httpOutcome = ex.ToOutcome();
            context.Response.StatusCode = httpOutcome.ErrorCode;
            await context.Response.WriteAsJsonAsync(httpOutcome.ErrorBody);
        }
    }
}
