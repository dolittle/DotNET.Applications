// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a system that knows about stream filters.
    /// </summary>
    public interface IStreamFilters
    {
        /// <summary>
        /// Check if there is a filter with the given <see cref="FilterId"/>.
        /// </summary>
        /// <param name="filterId"><see cref="FilterId"/> to check for.</param>
        /// <returns>true if there is one, false if not.</returns>
        bool HasFor(FilterId filterId);

        /// <summary>
        /// Get the <see cref="IEventStreamFilter"/> filter with a given <see cref="FilterId"/>.
        /// </summary>
        /// <param name="filterId"><see cref="FilterId"/> to get for.</param>
        /// <returns><see cref="IEventStreamFilter"/> instance.</returns>
        IEventStreamFilter GetFor(FilterId filterId);

        /// <summary>
        /// Registers the <see cref="IEventStreamFilter" />.
        /// </summary>
        /// <param name="filter">The <see cref="IEventStreamFilter" />.</param>
        void Register(IEventStreamFilter filter);

        /// <summary>
        /// Starts processing all filters.
        /// </summary>
        void StartProcessingFilters();
    }
}