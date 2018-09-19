/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Artifacts;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.PropertyBags;
using Dolittle.Reflection;
using Dolittle.Runtime.Events;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Runtime.Events.Store;
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
        readonly ILogger _logger;
        readonly ProcessMethodInvoker _invoker;

        /// <summary>
        /// Initializes a new instance of <see cref="ProcessMethodEventProcessor"/>
        /// </summary>
        /// <param name="objectFactory"><see cref="IObjectFactory"/> for going between <see cref="PropertyBag"/> and instances of types</param>
        /// <param name="container"><see cref="IContainer"/> to use for getting instances of <see cref="ICanProcessEvents"/> implementation</param>
        /// <param name="identifier"><see cref="EventProcessorId"/> that uniquely identifies the <see cref="ProcessMethodEventProcessor"/></param>
        /// <param name="eventIdentifier"><see cref="Artifact">Identifier</see> for identifying the <see cref="IEvent"/></param>
        /// <param name="eventType"><see cref="Type"/> type of <see cref="IEvent"/>></param>
        /// <param name="methodInfo"><see cref="MethodInfo"/> for the actual process method</param>
        /// <param name="logger"></param>
        public ProcessMethodEventProcessor(
            IObjectFactory objectFactory,
            IContainer container,
            EventProcessorId identifier,
            Artifact eventIdentifier,
            Type eventType,
            MethodInfo methodInfo,
            ILogger logger)
        {
            Identifier = identifier;
            Event = eventIdentifier;
            _logger = logger;
            _logger.Trace($"ProcessMethodEventProcessor for '{eventIdentifier}' exists on type '{methodInfo}'");
            _invoker = GetProcessorMethodInvokerFor(methodInfo,eventType,objectFactory, container,logger);
        }

        /// <inheritdoc/>
        public Artifact Event { get; }

        /// <inheritdoc/>
        public EventProcessorId Identifier { get; }

        /// <inheritdoc/>
        public void Process(CommittedEventEnvelope eventEnvelope)
        {
            _invoker.Process(eventEnvelope);
        }

        ProcessMethodInvoker GetProcessorMethodInvokerFor(MethodInfo method, Type eventType, IObjectFactory objectFactory, IContainer container, ILogger logger)
        {
            if(method.ReturnType != typeof(void))
                throw new EventProcessorMethodParameterMismatch();

            if(IsSingleEventParameterVersion(method, eventType))
                return new ProcessorMethodWithEvent(method,eventType,objectFactory,container,logger);

            if(IsEventParameterWithEventSourceIdVersion(method, eventType))
                return new ProcessorMethodWithEventAndEventSourceId(method,eventType,objectFactory,container,logger);

            if(IsEventParameterWithEventMetadataVersion(method, eventType))
                return new ProcessorMethodWithEventAndMetadata(method,eventType,objectFactory,container,logger);
                
            throw new EventProcessorMethodParameterMismatch();
        }

        static bool IsSingleEventParameterVersion(MethodInfo method, Type eventType)
        {
            var parameters = method.GetParameters();
            return parameters.Count() == 1 && parameters.First().ParameterType == eventType;
        }

        static bool IsEventParameterWithEventSourceIdVersion(MethodInfo method, Type eventType)
        {
            var parameters = method.GetParameters();
            return parameters.Count() == 2 
                    && parameters.First().ParameterType == eventType 
                    && parameters.Last().ParameterType == typeof(EventSourceId);
        }

        static bool IsEventParameterWithEventMetadataVersion(MethodInfo method, Type eventType)
        {
            var parameters = method.GetParameters();
            return parameters.Count() == 2 
                    && parameters.First().ParameterType == eventType 
                    && parameters.Last().ParameterType == typeof(EventMetadata);
        }


        private abstract class ProcessMethodInvoker 
        {
            private readonly MethodInfo _method;
            protected readonly IObjectFactory _objectFactory;
            protected Type _eventType;
            private readonly IContainer _container;
            private readonly ILogger _logger;

            protected ProcessMethodInvoker(MethodInfo method, Type eventType, IObjectFactory objectFactory, IContainer container, ILogger logger)
            {
                _method = method;
                _objectFactory = objectFactory;
                _eventType = eventType;
                _container = container;
                _logger = logger;
            }

            public void Process(CommittedEventEnvelope committedEventEnvelope)
            {
                try
                {
                    var processor = _container.Get(_method.DeclaringType);
                    _method.Invoke(processor, BuildParameters(committedEventEnvelope));
                }
                catch (Exception exception)
                {
                    _logger.Error(exception, "Failed processing");
                    throw;
                }
            }

            protected abstract object[] BuildParameters(CommittedEventEnvelope committedEventEnvelope);
        }

        private class ProcessorMethodWithEvent : ProcessMethodInvoker
        {
            public ProcessorMethodWithEvent(MethodInfo method, Type eventType, IObjectFactory objectFactory, IContainer container, ILogger logger)
                : base(method,eventType,objectFactory,container,logger)
            {
            }

            protected override object[] BuildParameters(CommittedEventEnvelope committedEventEnvelope)
            {
                object @event = _objectFactory.Build(_eventType, committedEventEnvelope.Event);
                return new[] { @event };
            }
        }  


        private class ProcessorMethodWithEventAndEventSourceId : ProcessMethodInvoker
        {
            public ProcessorMethodWithEventAndEventSourceId(MethodInfo method, Type eventType, IObjectFactory objectFactory, IContainer container, ILogger logger)
                : base(method,eventType,objectFactory,container,logger)
            {
            }

            protected override object[] BuildParameters(CommittedEventEnvelope committedEventEnvelope)
            {
                object @event = _objectFactory.Build(_eventType, committedEventEnvelope.Event);
                return new[] { @event, committedEventEnvelope.Metadata.EventSourceId };
            }
        }  

        private class ProcessorMethodWithEventAndMetadata : ProcessMethodInvoker
        {
            public ProcessorMethodWithEventAndMetadata(MethodInfo method, Type eventType, IObjectFactory objectFactory, IContainer container, ILogger logger)
                : base(method,eventType,objectFactory,container,logger)
            {
            }

            protected override object[] BuildParameters(CommittedEventEnvelope committedEventEnvelope)
            {
                object @event = _objectFactory.Build(_eventType, committedEventEnvelope.Event);
                return new[] { @event, committedEventEnvelope.Metadata };
            }
        }                        
    }
}