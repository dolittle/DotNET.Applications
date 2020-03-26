// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;

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
        /// De-registers the <see cref="IEventStreamFilter" />.
        /// </summary>
        /// <param name="filter">The <see cref="IEventStreamFilter" />.</param>
        void DeRegister(FilterId filter);

        /// <summary>
        /// Starts processing all filters.
        /// </summary>
        /// <param name="filter">The <see cref="IEventStreamFilter" />.</param>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Start(IEventStreamFilter filter, CancellationToken token = default);
    }
}