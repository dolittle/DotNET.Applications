/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Time;
using Dolittle.Types;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Logging;
using Dolittle.Artifacts;
using Dolittle.PropertyBags;
using Dolittle.Lifecycle;
using Dolittle.Collections;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IKnowAboutEventProcessors"/> for 
    /// <see cref="IEventProcessor">event processors</see> in the currently running process.
    /// </summary>
    /// <remarks>
    /// The <see cref="IEventProcessor">processors</see> this implementation deals with is your
    /// .NET based and discovered <see cref="IEventProcessor">processors</see>
    /// </remarks>
    [Singleton]
    public class ProcessMethodEventProcessors : IKnowAboutEventProcessors
    {
        /// <summary>
        /// The separator used in the <see cref="EventProcessorId"/> between the type and the event it handles
        /// </summary>
        public const string IdentifierSeparator = "|";

        /// <summary>
        /// Name of method that any event subscriber needs to be called in order to be recognized by the convention
        /// </summary>
        public const string ProcessMethodName = "Process";

        readonly List<IEventProcessor> _eventProcessors = new List<IEventProcessor>();
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IImplementationsOf<ICanProcessEvents> _processors;
        readonly IContainer _container;
        readonly ISystemClock _systemClock;
        readonly IObjectFactory _objectFactory;
        readonly ILogger _logger;


        /// <summary>
        /// Initializes a new instance of <see cref="ProcessMethodEventProcessors"/>
        /// </summary>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for identifying <see cref="IEvent">events</see> </param>
        /// <param name="objectFactory"><see cref="IObjectFactory"/> for going between <see cref="PropertyBag"/> and instances of types</param>
        /// <param name="processors"><see cref="IImplementationsOf{ICanProcessEvents}"/> for discovering implementations of <see cref="ICanProcessEvents"/></param>
        /// <param name="container"><see cref="IContainer"/> for the implementation <see cref="ProcessMethodEventProcessor"/> when acquiring instances of implementations of <see cref="ICanProcessEvents"/></param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for timing <see cref="IEventProcessors"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ProcessMethodEventProcessors(
            IArtifactTypeMap artifactTypeMap,
            IObjectFactory objectFactory,
            IImplementationsOf<ICanProcessEvents> processors,
            IContainer container,
            ISystemClock systemClock,
            ILogger logger)
        {
            _artifactTypeMap = artifactTypeMap;
            _processors = processors;
            _container = container;
            _systemClock = systemClock;
            _logger = logger;
            _objectFactory = objectFactory;
        }

        /// <inheritdoc/>
        public IEnumerator<IEventProcessor> GetEnumerator()
        {
            return _eventProcessors.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _eventProcessors.GetEnumerator();
        }


        /// <summary>
        /// Populates from in process event processors through discovery
        /// </summary>
        public void Populate()
        {
            foreach (var processor in _processors)
            {
                _logger.Trace($"Processor '{processor.AssemblyQualifiedName}'");
                processor.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList().ForEach(_ => _logger.Trace($"{_.Name} {_.EventProcessorId()}"));
                var methods = processor.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(m => m.EventProcessorId().Value != Guid.Empty).ToList();
                foreach (var method in methods)
                {
                    _logger.Trace($"Method found '{method} {method:EventProcessorId()}'");

                    var parameterType = method.GetParameters()[0].ParameterType;
                    var eventIdentifier = _artifactTypeMap.GetArtifactFor(parameterType);
                    var eventProcessorId = method.EventProcessorId();

                    _logger.Trace($"EventProcessor identifier '{eventProcessorId}'");

                    var processMethodEventProcessor = new ProcessMethodEventProcessor(
                                                            _objectFactory,
                                                            _container, 
                                                            eventProcessorId, 
                                                            eventIdentifier,
                                                            parameterType,
                                                            method, 
                                                            _logger);

                    _eventProcessors.Add(processMethodEventProcessor);
                }
            }
        }
    }
}
