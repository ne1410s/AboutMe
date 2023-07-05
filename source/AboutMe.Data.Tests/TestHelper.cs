// <copyright file="TestHelper.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Data.Tests;

using Microsoft.EntityFrameworkCore;

public static class TestHelper
{
    public static AboutMeContext GetDb(string? connectionId = null)
    {
        var optsBuilder = new DbContextOptionsBuilder<AboutMeContext>();
        optsBuilder.UseInMemoryDatabase(connectionId ?? Guid.NewGuid().ToString());
        return new AboutMeContext(optsBuilder.Options);
    }
}
