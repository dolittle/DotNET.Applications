// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an abstract implementation for a system that can invoke a <see cref="CommittedEvent" /> on an event handler of a specific event handler type.
    /// </summary>
    /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
    public abstract class AbstractEventHandlerForEventHandlerType<TEventHandler> : AbstractEventHandler
        where TEventHandler : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractEventHandlerForEventHandlerType{T}"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="identifier">The unique <see cref="EventHandlerId">identifier</see>.</param>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="partitioned">Whether the Event Handler is partitioned.</param>
        /// <param name="methods"><see cref="IEnumerable{T}"/> of <see cref="EventHandlerMethod{T}"/>.</param>
        protected AbstractEventHandlerForEventHandlerType(
            IContainer container,
            ScopeId scope,
            EventHandlerId identifier,
            Type type,
            bool partitioned,
            IEnumerable<IEventHandlerMethod> methods)
            : base(container, scope, identifier, type, partitioned, methods)
        {
            ThrowIfTypeIsNotCorrectEventHandlerType();
        }

        void ThrowIfTypeIsNotCorrectEventHandlerType()
        {
            if (!Type.Implements(typeof(TEventHandler)))
            {
                throw new IncorrectEventHandlerType(Type, typeof(TEventHandler));
            }
        }
    }
}