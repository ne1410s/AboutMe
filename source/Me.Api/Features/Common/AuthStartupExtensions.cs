// <copyright file="AuthStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.Features.Common;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

/// <summary>
/// Extensions for configuring auth at startup.
/// </summary>
public static class AuthStartupExtensions
{
    /// <summary>
    /// Adds the auth feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The config.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddAuthFeature(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(configuration);

        return services;
    }

    /// <summary>
    /// Uses the auth feature.
    /// </summary>
    /// <param name="app">The app builder.</param>
    /// <returns>The same app builder.</returns>
    public static IApplicationBuilder UseAuthFeature(
        this WebApplication app) => app
            .UseAuthentication()
            .UseAuthorization();
}
