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
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly IImplementationsOf<ICanHandleEvents> _eventHandlerTypes;
        readonly IEventHandlers _eventHandlers;
        readonly IEventHandlersWaiters _eventHandlersWaiters;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes"><see cref="IImplementationsOf{T}"/> <see cref="ICanHandleEvents"/>.</param>
        /// <param name="eventHandlers">The <see cref="IEventHandlers"/> system.</param>
        /// <param name="eventHandlersWaiters"><see cref="IEventHandlersWaiters"/> for registering event handlers.</param>
        public BootProcedure(
            IImplementationsOf<ICanHandleEvents> eventHandlerTypes,
            IEventHandlers eventHandlers,
            IEventHandlersWaiters eventHandlersWaiters)
        {
            _eventHandlerTypes = eventHandlerTypes;
            _eventHandlers = eventHandlers;
            _eventHandlersWaiters = eventHandlersWaiters;
        }

        /// <inheritdoc/>
        public bool CanPerform() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public void Perform()
        {
            _eventHandlerTypes.ForEach(type =>
            {
                var eventHandler = _eventHandlers.Register(type);
                _eventHandlersWaiters.RegisterHandler(eventHandler);
            });
        }
    }
}