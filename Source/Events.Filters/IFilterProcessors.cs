// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a system that knows about <see cref="IFilterProcessor" />.
    /// </summary>
    public interface IFilterProcessors
    {
        /// <summary>
        /// Start processing for a specific <see cref="IEventStreamFilter"> filter</see>.
        /// </summary>
        /// <param name="filter"><see cref="IEventStreamFilter"/> to start processing.</param>
        void Start(IEventStreamFilter filter);
    }
}