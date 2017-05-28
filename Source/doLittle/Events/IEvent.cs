/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Conventions;

namespace doLittle.Events
{
    /// <summary>
    /// Defines the basics of an event.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface can be used in event sourcing and will be picked up by the event migration system.
    /// You most likely want to subclass <see cref="Event"/>.
    /// </remarks>
    public interface IEvent : IConvention
    {
        /// <summary>
        /// Gets the <see cref="EventSourceId"/> for the <see cref="IEventSource"/> the event originates from
        /// </summary>
        EventSourceId EventSourceId { get; set; }
    }
}
