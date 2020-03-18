// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
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
        readonly IInstancesOf<ICanProvideEventHandlers> _eventHandlerProviders;
        readonly IEventHandlers _eventHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="eventHandlerProviders"><see cref="IInstancesOf{T}"/> <see cref="ICanProvideEventHandlers"/>.</param>
        /// <param name="eventHandlers">The <see cref="IEventHandlers"/> system.</param>
        public BootProcedure(
            IInstancesOf<ICanProvideEventHandlers> eventHandlerProviders,
            IEventHandlers eventHandlers)
        {
            _eventHandlerProviders = eventHandlerProviders;
            _eventHandlers = eventHandlers;
        }

        /// <inheritdoc/>
        public bool CanPerform() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public void Perform()
        {
            _eventHandlerProviders.SelectMany(provider => provider.Provide()).ForEach(eventHandler => _eventHandlers.Register(eventHandler));
            _eventHandlers.StartProcessingEventHandlers();
        }
    }
}