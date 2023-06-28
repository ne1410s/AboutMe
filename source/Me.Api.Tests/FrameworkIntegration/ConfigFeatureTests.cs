// <copyright file="ConfigFeatureTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.tests.FrameworkIntegration;

using System.Net.Http.Json;
using Me.Api.Features.Config;

/// <summary>
/// Tests for endpoints defined in the <see cref="ConfigController"/> class.
/// </summary>
public class ConfigFeatureTests
{
    private readonly HttpClient client;

    public ConfigFeatureTests()
    {
        var appFactory = new TestingWebAppFactory();
        this.client = appFactory.CreateClient();
    }

    [Fact]
    public async Task GetSettings_WhenCalled_ReturnsExpected()
    {
        // Arrange
        const string serviceUrl = "config/settings";
        var expected = new DynamicSettings
        {
            Global = new() { GlobalTestSetting1 = "global-ts1-test" },
            Nutrition = new() { LocalTestSetting1 = "nutrition-ts1-test" },
        };

        // Act
        var response = await this.client.GetAsync(serviceUrl);
        var result = await response.Content.ReadFromJsonAsync<DynamicSettings>();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetFeatures_WhenCalled_ReturnsExpected()
    {
        // Arrange
        const string serviceUrl = "config/features";
        var expected = new Dictionary<string, FeatureWebModel>
        {
            ["TestFeature1"] = new("TestFeature1", "Test feature 1.", false),
            ["TestFeature2"] = new("TestFeature2", "Test feature 2.", true),
            ["TestFeature3"] = new("TestFeature3", null, false),
            ["TestFeature4"] = new("TestFeature4", null, false),
        };

        // Act
        var response = await this.client.GetAsync(serviceUrl);
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, FeatureWebModel>>();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetFeatureStatus_WhenCalled_ReturnsExpected()
    {
        // Arrange
        const string serviceUrl = "config/features/testfeature2";
        const string expected = "true";

        // Act
        var response = await this.client.GetAsync(serviceUrl);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        result.Should().Be(expected);
    }
}
