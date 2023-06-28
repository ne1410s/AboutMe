// <copyright file="IDateTimeService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.Features.Common;

/// <summary>
/// Date time service.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets the current date and time, offset from UTC.
    /// </summary>
    /// <returns>Current date and time offset.</returns>
    public DateTimeOffset OffsetNow();
}
