// <copyright file="MiddlewareFeaturesTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.Tests.FrameworkIntegration;

/// <summary>
/// Tests for miscellaneous http middleware.
/// </summary>
public class MiddlewareFeaturesTests : IDisposable
{
    private readonly TestingWebAppFactory appFactory = new();
    private readonly HttpClient client;

    public MiddlewareFeaturesTests()
    {
        this.client = this.appFactory.CreateClient();
    }

    [Fact]
    public async Task UnsecureHttp_WhenRequested_HandledAsExpected()
    {
        // Arrange
        var serviceUrl = new Uri("http://localhost:80/forecasts");

        // Act
        var response = await this.client.GetAsync(serviceUrl);

        // Assert
        response.RequestMessage!.RequestUri!.Scheme.ShouldBe("https");
    }

    [Fact]
    public async Task InvalidRequest_WhenRequested_HandledAsExpected()
    {
        // Arrange
        var serviceUrl = new Uri("forecasts?empirical=42", UriKind.Relative);

        // Act
        var response = await this.client.GetAsync(serviceUrl);

        // Assert
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ValidRequest_WhenRequested_ReturnsSuccess()
    {
        // Arrange
        var serviceUrl = new Uri("forecasts", UriKind.Relative);

        // Act
        var response = await this.client.GetAsync(serviceUrl);

        // Assert
        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.client.Dispose();
        this.appFactory.Dispose();
    }
}