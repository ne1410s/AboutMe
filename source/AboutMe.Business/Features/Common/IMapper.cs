// <copyright file="IMapper.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace AboutMe.Business.Features.Common;

/// <summary>
/// Maps from source to target.
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The target type.</typeparam>
public interface IMapper<in TSource, out TTarget>
{
    /// <summary>
    /// Maps from source to target.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns>The target.</returns>
    public TTarget Map(TSource source);
}
