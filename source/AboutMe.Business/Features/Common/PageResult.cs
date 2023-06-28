// <copyright file="PageResult.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Common;

/// <summary>
/// A page result.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
public record PageResult<T>
{
    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the items in the page.
    /// </summary>
    public IEnumerable<T> Data { get; set; } = Array.Empty<T>();

    /// <summary>
    /// Gets or sets the total pages.
    /// </summary>
    public int TotalPages { get; set; } = 1;

    /// <summary>
    /// Gets or sets the total records.
    /// </summary>
    public int TotalRecords { get; set; }
}
