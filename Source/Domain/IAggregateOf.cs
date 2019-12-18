/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain
{
    /// <summary>
    /// Defines a way to work with <see cref="IAggregateRoot"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IAggregateRoot"/> type</typeparam>
    public interface IAggregateOf<T>
        where T : class, IAggregateRoot
    {
        /// <summary>
        /// Create a new <see cref="IAggregateRoot"/> with a new <see cref="EventSourceId"/>
        /// </summary>
        /// <returns><see cref="IAggregateRootOperations{T}">Operations</see> that can be performed</returns>
        IAggregateRootOperations<T> Create();

        /// <summary>
        /// Create a new <see cref="IAggregateRoot"/> with a given <see cref="EventSourceId"/>
        /// </summary>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> of the <see cref="IAggregateRoot"/></param>
        /// <returns><see cref="IAggregateRootOperations{T}">Operations</see> that can be performed</returns>
        IAggregateRootOperations<T> Create(EventSourceId eventSourceId);

        /// <summary>
        /// Rehydrate an existing <see cref="IAggregateRoot"/> with a given <see cref="EventSourceId"/>
        /// </summary>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> of the <see cref="IAggregateRoot"/></param>
        /// <returns><see cref="IAggregateRootOperations{T}">Operations</see> that can be performed</returns>
        IAggregateRootOperations<T> Rehydrate(EventSourceId eventSourceId);
    }
}
