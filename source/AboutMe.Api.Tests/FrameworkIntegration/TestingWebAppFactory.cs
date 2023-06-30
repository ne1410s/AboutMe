// <copyright file="TestingWebAppFactory.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Tests.FrameworkIntegration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? onConfiguringServices;

    public TestingWebAppFactory(
        Action<IServiceCollection>? onConfiguringServices = null)
    {
        this.onConfiguringServices = onConfiguringServices;
    }

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
            this.onConfiguringServices?.Invoke(services));
    }
}
