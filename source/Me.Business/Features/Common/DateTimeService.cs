// <copyright file="DateTimeService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.Features.Common;

/// <inheritdoc cref="IDateTimeService"/>
public class DateTimeService : IDateTimeService
{
    /// <inheritdoc/>
    public DateTimeOffset OffsetNow() => DateTimeOffset.Now;
}
