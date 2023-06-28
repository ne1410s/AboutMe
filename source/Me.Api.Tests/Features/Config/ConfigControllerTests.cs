// <copyright file="ConfigControllerTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Api.tests.Features.Config;

using Me.Api.Features.Config;

/// <summary>
/// Tests for the <see cref="ConfigController"/> class.
/// </summary>
public class ConfigControllerTests
{
    [Fact]
    public void Ctor_NullOptions_Populates()
    {
        // Arrange & Act
        var sut = new ConfigController(null!, null!, null!);

        // Assert
        sut.GetSettings().Should().BeNull();
        sut.GetFeatures().Should().BeNull();
    }
}
