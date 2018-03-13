/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Runtime.Events;

namespace Dolittle.Domain
{
    /// <summary>
    /// Defines the basic functionality for finding and getting aggregated roots
    /// </summary>
    /// <typeparam name="T">Type of aggregated root</typeparam>
    public interface IAggregateRootRepositoryFor<T>
        where T : AggregateRoot
    {
        /// <summary>
        /// Get an aggregated root by id
        /// </summary>
        /// <param name="id"><see cref="EventSourceId"/> of aggregated root to get</param>
        /// <returns>An instance of the aggregated root</returns>
        T Get(EventSourceId id);
    }
}
