// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api;

using System.Diagnostics.CodeAnalysis;
using AboutMe.Api.Features.Common;

/// <summary>
/// The program. Exposing the type serves to support framework testing.
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Program"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    private Program() { }

    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">The args.</param>
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration config = builder.Configuration;
        builder.Services.AddControllers();
        builder.Services.AddCorsFeature(config);
        builder.Services.AddTracingFeature(config);
        builder.Services.AddSwaggerFeature();
        builder.Services.AddFluentErrorsFeature();
        builder.Services.AddSharedServices();
        builder.Services.AddHealthChecksFeature();

        var app = builder.Build();
        app.UseSwaggerFeature();
        app.UseHttpsRedirection();
        app.UseCorsFeature();
        app.UseFluentErrorsFeature();
        app.MapControllers();
        app.UseHealthChecksFeature();
        app.UseHsts();
        app.Run();
    }
}