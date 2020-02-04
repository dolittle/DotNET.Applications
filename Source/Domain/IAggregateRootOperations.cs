// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Domain
{
    /// <summary>
    /// Defines a system for working with operations that can be performed on an
    /// <see cref="IAggregateRoot"/>.
    /// </summary>
    /// <typeparam name="TAggregate"><see cref="IAggregateRoot"/> type.</typeparam>
    public interface IAggregateRootOperations<TAggregate>
        where TAggregate : class, IAggregateRoot
    {
        /// <summary>
        /// Perform an operation on an <see cref="IAggregateRoot"/>.
        /// </summary>
        /// <param name="method"><see cref="Action{T}">Method</see> to perform.</param>
        /// <returns>The <see cref="AggregateRootPerformResult">result</see>.</returns>
        AggregateRootPerformResult Perform(Action<TAggregate> method);
    }
}
