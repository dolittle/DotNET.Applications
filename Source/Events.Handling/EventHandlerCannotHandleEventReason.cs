// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents the reason for why an event handler cannot handle the event.
    /// </summary>
    public class EventHandlerCannotHandleEventReason : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string" /> to <see cref="EventHandlerCannotHandleEventReason" />.
        /// </summary>
        /// <param name="reason">The reason why it cannot handle event.</param>
        public static implicit operator EventHandlerCannotHandleEventReason(string reason) => new EventHandlerCannotHandleEventReason { Value = reason };
    }
}