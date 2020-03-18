// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.DependencyInversion;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="AbstractEventHandlerForEventHandlerType{T}" /> that can invoke events on instances of <see cref="ICanHandleExternalEvents"/>.
    /// </summary>
    public class ExternalEventHandler : AbstractEventHandlerForEventHandlerType<ICanHandleExternalEvents>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandler"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="identifier">The unique <see cref="EventHandlerId">identifier</see>.</param>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="methods"><see cref="IEnumerable{T}"/> of <see cref="EventHandlerMethod"/>.</param>
        public ExternalEventHandler(IContainer container, EventHandlerId identifier, Type type, IEnumerable<IEventHandlerMethod> methods)
        : base(container, identifier, type, false, methods)
        {
        }

        /// <summary>
        /// Gets the <see cref="Microservice" /> that the <see cref="ICanHandleExternalEvents" /> event handler handles events from.
        /// </summary>
        public Microservice Microservice { get; }

        /// <inheritdoc/>
        protected override void ThrowIfCannotInvoke(CommittedEvent @event)
        {
            if (@event.Microservice != Microservice) throw new ExternalEventHandlerCannotHandleEventsComingFromMicroservice(this, @event);
            base.ThrowIfCannotInvoke(@event);
        }
    }
}