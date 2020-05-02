// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Protobuf;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when a failure occurs during registration of an event handler.
    /// </summary>
    public class EventHandlerRegistrationFailed : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerRegistrationFailed"/> class.
        /// </summary>
        /// <param name="id">The unique <see cref="EventHandlerId"/> for the filter.</param>
        /// <param name="registerFailure">The <see cref="Failure"/> that occured.</param>
        public EventHandlerRegistrationFailed(EventHandlerId id, Failure registerFailure)
            : base($"Failure occured during registration of event handler {id}. {registerFailure.Reason}")
        {
        }
    }
}