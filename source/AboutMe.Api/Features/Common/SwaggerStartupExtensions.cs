﻿// <copyright file="SwaggerStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Features.Common;

using System.Reflection;
using FluentErrors.Extensions;

/// <summary>
/// Extensions for configuring Swagger at startup.
/// </summary>
public static class SwaggerStartupExtensions
{
    /// <summary>
    /// Adds the Swagger feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddSwaggerFeature(
        this IServiceCollection services) => services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                var asmName = Assembly.GetExecutingAssembly().GetName();
                var version = "v" + asmName.Version!.ToString(1);
                var xmlFile = asmName.Name + ".xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.SwaggerDoc(version, new() { Title = asmName.Name, Version = version });
                c.IncludeXmlComments(xmlPath);
            });

    /// <summary>
    /// Uses the Swagger feature.
    /// </summary>
    /// <param name="app">The app builder.</param>
    /// <returns>The same app builder.</returns>
    public static IApplicationBuilder UseSwaggerFeature(
        this WebApplication app)
    {
        app.MustExist();
        var env = app.Environment.EnvironmentName.ToLower();
        if (env == "local")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
