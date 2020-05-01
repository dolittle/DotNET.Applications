// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Filters.EventHorizon
{
    /// <summary>
    /// Defines a system that can filter instances of <see cref="IPublicEvent"/> to a public stream.
    /// </summary>
    public interface ICanFilterPublicEvents
    {
        /// <summary>
        /// Method that will be called to determine whether an <see cref="IPublicEvent"/> should be part of the public stream or not.
        /// </summary>
        /// <param name="event">The <see cref="IPublicEvent"/> to filter.</param>
        /// <param name="context">The <see cref="EventContext"/> of the event to filter.</param>
        /// <returns>A <see cref="Task{T}"/> of type <see cref="PublicFilterResult"/> representing the result of the asynchronous operation.</returns>
        Task<PublicFilterResult> Filter(IPublicEvent @event, EventContext context);
    }
}