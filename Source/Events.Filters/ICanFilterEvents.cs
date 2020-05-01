// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a system that can filter instances of <see cref="IEvent"/> to a stream.
    /// </summary>
    public interface ICanFilterEvents
    {
        /// <summary>
        /// Method that will be called to determine whether an <see cref="IEvent"/> should be part of the stream or not.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/> to filter.</param>
        /// <param name="context">The <see cref="EventContext"/> of the event to filter.</param>
        /// <returns>A <see cref="Task{T}"/> of type <see cref="FilterResult"/> representing the result of the asynchronous operation.</returns>
        Task<FilterResult> Filter(IEvent @event, EventContext context);
    }
}