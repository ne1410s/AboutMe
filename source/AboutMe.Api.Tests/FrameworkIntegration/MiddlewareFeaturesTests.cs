// <copyright file="MiddlewareFeaturesTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Api.tests.FrameworkIntegration;

using System.Net;
using System.Net.Http.Json;
using FluentErrors.Api.Models;

/// <summary>
/// Tests for miscellaneous http middleware.
/// </summary>
public class MiddlewareFeaturesTests
{
    private readonly HttpClient client;

    public MiddlewareFeaturesTests()
    {
        var appFactory = new TestingWebAppFactory(nameof(MiddlewareFeaturesTests));
        this.client = appFactory.CreateClient();
    }

    [Fact]
    public async Task UnsecureHttp_WhenRequested_HandledAsExpected()
    {
        // Arrange
        const string serviceUrl = "http://localhost:80/forecast";

        // Act
        var response = await this.client.GetAsync(serviceUrl);

        // Assert
        response.RequestMessage!.RequestUri!.Scheme.Should().Be("https");
    }

    [Fact]
    public async Task ModelItem_WhenInvalid_ReturnsExpected()
    {
        // Arrange
        const string serviceUrl = "items";
        var request = new { name = "test", specificFat = "hello" };
        var expected = new HttpErrorBody("StaticValidationException", "Invalid instance received.");

        // Act
        var response = await this.client.PostAsJsonAsync(serviceUrl, request);
        var result = await response.Content.ReadFromJsonAsync<HttpErrorBody>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Should().BeEquivalentTo(expected, x => x.Excluding(d => d.Errors));
    }
}