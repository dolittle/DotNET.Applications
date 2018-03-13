/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Applications;
using Dolittle.Execution;
using Dolittle.DependencyInversion;
using Dolittle.Time;
using Dolittle.Types;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Logging;

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
        /// The separator used in the <see cref="EventProcessorIdentifier"/> between the type and the event it handles
        /// </summary>
        public const string IdentifierSeparator = "|";

        /// <summary>
        /// Name of method that any event subscriber needs to be called in order to be recognized by the convention
        /// </summary>
        public const string ProcessMethodName = "Process";

        List<IEventProcessor> _eventProcessors = new List<IEventProcessor>();

        IApplicationArtifacts _applicationArtifacts;
        IApplicationArtifactIdentifierStringConverter _applicationArtifactIdentifierStringConverter;
        ITypeFinder _typeFinder;
        IContainer _container;
        ISystemClock _systemClock;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ProcessMethodEventProcessors"/>
        /// </summary>
        /// <param name="applicationArtifacts"><see cref="IApplicationArtifacts"/> for identifying <see cref="IEvent">events</see> </param>
        /// <param name="applicationArtifactIdentifierStringConverter"><see cref="IApplicationArtifactIdentifierStringConverter"/> for converting <see cref="IApplicationArtifactIdentifier"/> to and from different formats</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for discovering implementations of <see cref="ICanProcessEvents"/></param>
        /// <param name="container"><see cref="IContainer"/> for the implementation <see cref="ProcessMethodEventProcessor"/> when acquiring instances of implementations of <see cref="ICanProcessEvents"/></param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for timing <see cref="IEventProcessors"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ProcessMethodEventProcessors(
            IApplicationArtifacts applicationArtifacts,
            IApplicationArtifactIdentifierStringConverter applicationArtifactIdentifierStringConverter,
            ITypeFinder typeFinder,
            IContainer container,
            ISystemClock systemClock,
            ILogger logger)
        {
            _applicationArtifacts = applicationArtifacts;
            _applicationArtifactIdentifierStringConverter = applicationArtifactIdentifierStringConverter;
            _typeFinder = typeFinder;
            _container = container;
            _systemClock = systemClock;
            _logger = logger;

            PopulateEventProcessors();
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

        void PopulateEventProcessors()
        {
            var processors = _typeFinder.FindMultiple<ICanProcessEvents>();
            foreach (var processor in processors)
            {
                _logger.Trace($"Processor '{processor.AssemblyQualifiedName}'");

                var methods = processor.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(m =>
                {
                    var parameters = m.GetParameters();
                    return
                        m.Name.Equals(ProcessMethodName) &&
                        parameters.Length == 1 &&
                        typeof(IEvent).GetTypeInfo().IsAssignableFrom(parameters[0].ParameterType.GetTypeInfo());
                });

                foreach (var method in methods)
                {
                    _logger.Trace($"Method found '{method}'");

                    var eventProcessorTypeIdentifier = _applicationArtifacts.Identify(processor);
                    _logger.Trace($"Processor identified as '{eventProcessorTypeIdentifier}'");

                    var eventProcessorTypeIdentifierAsString = _applicationArtifactIdentifierStringConverter.AsString(eventProcessorTypeIdentifier);
                    var eventIdentifier = _applicationArtifacts.Identify(method.GetParameters()[0].ParameterType);
                    var eventIdentifierAsString = _applicationArtifactIdentifierStringConverter.AsString(eventIdentifier);
                    var eventProcessorIdentifier = (EventProcessorIdentifier)$"{eventProcessorTypeIdentifierAsString}{IdentifierSeparator}{eventIdentifierAsString}";

                    _logger.Trace($"EventProcessor identifier '{eventProcessorIdentifier}'");

                    var processMethodEventProcessor = new ProcessMethodEventProcessor(_container, _systemClock, eventProcessorIdentifier, eventIdentifier, method, _logger);
                    _eventProcessors.Add(processMethodEventProcessor);
                }
            }
        }
    }
}
