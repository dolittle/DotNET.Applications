// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a system that can handle processing for a <see cref="IEventStreamFilter"/>.
    /// </summary>
    public interface IFilterProcessor
    {
        /// <summary>
        /// Whether this <see cref="IEventStreamFilter" /> can be processed.
        /// </summary>
        /// <param name="filter">The <see cref="IEventStreamFilter" />.</param>
        /// <returns>true if it can be processed, false if not.</returns>
        bool CanProcess(IEventStreamFilter filter);

        /// <summary>
        /// Start processing for a specific <see cref="IEventStreamFilter"> filter</see>.
        /// </summary>
        /// <param name="filter"><see cref="IEventStreamFilter"/> to start processing.</param>
        void Start(IEventStreamFilter filter);
    }
}