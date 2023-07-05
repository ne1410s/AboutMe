// <copyright file="TestingWebAppFactory.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Tests.FrameworkIntegration;

using AboutMe.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? onConfiguringServices;

    public TestingWebAppFactory(
        string? connectionId = null,
        Action<IServiceCollection>? onConfiguringServices = null)
    {
        var optsBuilder = new DbContextOptionsBuilder<AboutMeContext>();
        optsBuilder.UseInMemoryDatabase(connectionId ?? Guid.NewGuid().ToString());
        this.Db = new AboutMeContext(optsBuilder.Options);

        this.onConfiguringServices = onConfiguringServices;
    }

    public AboutMeContext Db { get; }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(opts => opts
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Path.Combine(nameof(FrameworkIntegration), "appsettings.test.json"), false)
            .Build());

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("https_port", "5001");
        builder.UseEnvironment("test");
        builder.ConfigureServices(services =>
        {
            // Remove ef database connections
            RemoveServices<DbContextOptions>(services);
            services.AddSingleton(_ => this.Db);

            this.onConfiguringServices?.Invoke(services);
        });
    }

    private static void RemoveServices<T>(IServiceCollection services)
    {
        var removeList = services.Where(d => d.ServiceType == typeof(T)).ToList();
        foreach (var descriptor in removeList)
        {
            services.Remove(descriptor);
        }
    }
}
