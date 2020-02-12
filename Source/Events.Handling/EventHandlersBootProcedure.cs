// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents a <see cref="ICanPerformBootProcedure"/> for settings up event handlers.
    /// </summary>
    public class EventHandlersBootProcedure : ICanPerformBootProcedure
    {
        readonly IImplementationsOf<ICanHandleEvents> _eventHandlerTypes;
        readonly IEventHandlers _eventHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlersBootProcedure"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes"><see cref="IImplementationsOf{T}"/> <see cref="ICanHandleEvents"/>.</param>
        /// <param name="eventHandlers">The <see cref="IEventHandlers"/> system.</param>
        public EventHandlersBootProcedure(
            IImplementationsOf<ICanHandleEvents> eventHandlerTypes,
            IEventHandlers eventHandlers)
        {
            _eventHandlerTypes = eventHandlerTypes;
            _eventHandlers = eventHandlers;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _eventHandlerTypes.ForEach(type => _eventHandlers.Register(type));
        }
    }
}