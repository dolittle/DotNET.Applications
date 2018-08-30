/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Dolittle.Artifacts;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.PropertyBags;
using Dolittle.Reflection;
using Dolittle.Runtime.Events;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Time;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessor"/> for systems marked with the
    /// <see cref="ICanProcessEvents"/> marker interface and has the "Process" method according to the
    /// convention
    /// </summary>
    public class ProcessMethodEventProcessor : IEventProcessor
    {
        readonly IObjectFactory _objectFactory;
        readonly IContainer _container;
        readonly MethodInfo _methodInfo;
        readonly ILogger _logger;
        readonly Type _eventType;

        /// <summary>
        /// Initializes a new instance of <see cref="ProcessMethodEventProcessor"/>
        /// </summary>
        /// <param name="objectFactory"><see cref="IObjectFactory"/> for going between <see cref="PropertyBag"/> and instances of types</param>
        /// <param name="container"><see cref="IContainer"/> to use for getting instances of <see cref="ICanProcessEvents"/> implementation</param>
        /// <param name="identifier"><see cref="EventProcessorIdentifier"/> that uniquely identifies the <see cref="ProcessMethodEventProcessor"/></param>
        /// <param name="eventIdentifier"><see cref="Artifact">Identifier</see> for identifying the <see cref="IEvent"/></param>
        /// <param name="eventType"><see cref="Type"/> type of <see cref="IEvent"/>></param>
        /// <param name="methodInfo"><see cref="MethodInfo"/> for the actual process method</param>
        /// <param name="logger"></param>
        public ProcessMethodEventProcessor(
            IObjectFactory objectFactory,
            IContainer container,
            EventProcessorIdentifier identifier,
            Artifact eventIdentifier,
            Type eventType,
            MethodInfo methodInfo,
            ILogger logger)
        {
            _container = container;
            Identifier = identifier;
            _eventType = eventType;
            Event = eventIdentifier;
            _methodInfo = methodInfo;
            _logger = logger;
            _logger.Trace($"ProcessMethodEventProcessor for '{eventIdentifier}' exists on type '{methodInfo}'");
            _objectFactory = objectFactory;
        }

        /// <inheritdoc/>
        public Artifact Event { get; }

        /// <inheritdoc/>
        public EventProcessorIdentifier Identifier { get; }

        /// <inheritdoc/>
        public void Process(EventEnvelope eventEnvelope)
        {
            try
            {
                var processor = _container.Get(_methodInfo.DeclaringType);
                object @event;
                @event = _objectFactory.Build(_eventType, eventEnvelope.Event);
                
                _methodInfo.Invoke(processor, new[] { @event });
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Failed processing");
            }
        }
    }
}