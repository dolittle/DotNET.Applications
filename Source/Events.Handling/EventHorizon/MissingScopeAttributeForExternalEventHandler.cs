// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="ICanHandleExternalEvents">event handler</see> does not have the <see cref="ScopeAttribute"/>.
    /// </summary>
    public class MissingScopeAttributeForExternalEventHandler : MissingAttributeForEventHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingScopeAttributeForExternalEventHandler"/> class.
        /// </summary>
        /// <param name="eventHandlerType">Type of <see cref="ICanHandleExternalEvents"/>.</param>
        public MissingScopeAttributeForExternalEventHandler(Type eventHandlerType)
            : base(eventHandlerType, typeof(ScopeAttribute), $"\"{Guid.Empty}\"")
        {
        }
    }
}