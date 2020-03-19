// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.DependencyInversion;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="AbstractEventHandlerForEventHandlerType{T}" /> that can invoke events on instances of <see cref="ICanHandleEvents"/>.
    /// </summary>
    public class EventHandler : AbstractEventHandlerForEventHandlerType<ICanHandleEvents>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="identifier">The unique <see cref="EventHandlerId">identifier</see>.</param>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="partitioned">Whether the Event Handler is partitioned.</param>
        /// <param name="methods"><see cref="IEnumerable{T}"/> of <see cref="EventHandlerMethod{T}"/>.</param>
        public EventHandler(
            IContainer container,
            EventHandlerId identifier,
            Type type,
            bool partitioned,
            IEnumerable<IEventHandlerMethod> methods)
            : base(container, identifier, type, partitioned, methods)
        {
        }
    }
}