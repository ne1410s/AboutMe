// <copyright file="TestingWebAppFactory.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.tests.FrameworkIntegration;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? onConfiguringServices;
    private readonly ClaimsIdentity mockUser;

    public TestingWebAppFactory(
        Action<IServiceCollection>? onConfiguringServices = null,
        ClaimsIdentity? mockUser = null)
    {
        this.onConfiguringServices = onConfiguringServices;
        this.mockUser = mockUser ?? new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.GivenName, "Teddy"),
            new Claim(ClaimTypes.Surname, "Test"),
        });
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
        {
            // Disable authz
            RemoveServices<IAuthorizationHandler>(services);
            services.AddSingleton<IAuthorizationHandler>(_ => new Impersonator(this.mockUser));

            this.onConfiguringServices?.Invoke(services);
        });
    }

    private static void RemoveServices<T>(IServiceCollection services)
    {
        foreach (var descriptor in services.Where(d => d.ServiceType == typeof(T)).ToList())
        {
            services.Remove(descriptor);
        }
    }

    private class Impersonator : IAuthorizationHandler
    {
        private readonly ClaimsIdentity mockUser;

        public Impersonator(ClaimsIdentity mockUser)
        {
            this.mockUser = mockUser;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var requirement in context.PendingRequirements.ToList())
            {
                context.Succeed(requirement);
            }

            context.User.AddIdentity(this.mockUser);
            return Task.CompletedTask;
        }
    }
}
