// <copyright file="PageExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Common;

/// <summary>
/// Extensions for paging.
/// </summary>
public static class PageExtensions
{
    /// <summary>
    /// Directly pages a sequence of items.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="items">The entire item sequence.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>The result of the specified page.</returns>
    public static PageResult<T> Page<T>(
        this IEnumerable<T> items,
        int pageNumber = 1,
        int pageSize = 10)
    {
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Max(1, pageSize);

        var totalRecords = items.Count();
        var data = items
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToList();

        return new PageResult<T>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = Math.Max(1, (int)Math.Ceiling((decimal)totalRecords / pageSize)),
            TotalRecords = totalRecords,
        };
    }

    /// <summary>
    /// Pages a sequence of items, mapping the items on the resulting page.
    /// </summary>
    /// <typeparam name="TSource">The source item type.</typeparam>
    /// <typeparam name="TMapped">The mapped item type.</typeparam>
    /// <param name="paged">The paged items.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>The result of the specified page.</returns>
    public static PageResult<TMapped> MapTo<TSource, TMapped>(
        this PageResult<TSource> paged,
        Func<TSource, TMapped> mapper)
    {
        return paged == null ? null! : new PageResult<TMapped>
        {
            Data = paged.Data.Select(mapper).ToList(),
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalPages = paged.TotalPages,
            TotalRecords = paged.TotalRecords,
        };
    }
}