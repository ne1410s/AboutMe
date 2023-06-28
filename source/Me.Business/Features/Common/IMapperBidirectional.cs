// <copyright file="IMapperBidirectional.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Me.Business.Features.Common;

/// <summary>
/// Maps from source to target and vice versa.
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The target type.</typeparam>
public interface IMapperBidirectional<TSource, TTarget> : IMapper<TSource, TTarget>
{
    /// <summary>
    /// Maps from target to source.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <returns>The source.</returns>
    public TSource MapBack(TTarget target);
}
