// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents a system that can filter a stream of events.
    /// </summary>
    public interface IEventStreamFilter
    {
        /// <summary>
        /// Method that is asked if event is accepted.
        /// </summary>
        /// <param name="event"><see cref="CommittedEvent"/> to ask if is accepted.</param>
        /// <returns>true if accepted, false if not.</returns>
        Task<FilterResult> Filter(CommittedEvent @event);
    }
}