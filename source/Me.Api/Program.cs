// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api;

using System.Diagnostics.CodeAnalysis;
using Me.Api.Features.Common;
using Me.Api.Features.Item;
using Me.Api.Features.Recipe;

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
        builder.AddAppConfigFeature();
        builder.Services.AddAuthFeature(config);
        builder.Services.AddControllers();
        builder.Services.AddCorsFeature(config);
        builder.Services.AddTracingFeature(config);
        builder.Services.AddSwaggerFeature();
        builder.Services.AddFluentErrorsFeature();
        builder.Services.AddDatabaseFeature(config);
        builder.Services.AddSharedServices();
        builder.Services.AddItemFeature();
        builder.Services.AddRecipeFeature();
        builder.Services.AddHealthChecksFeature(config);

        var app = builder.Build();
        app.UseAppConfigFeature(config);
        app.UseSwaggerFeature();
        app.UseHttpsRedirection();
        app.UseCorsFeature();
        app.UseAuthFeature();
        app.UseFluentErrorsFeature();
        app.MapControllers().RequireAuthorization();
        app.UseHealthChecksFeature();
        app.Run();
    }
}