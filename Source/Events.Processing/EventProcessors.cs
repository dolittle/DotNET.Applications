/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Reflection;
using Dolittle.Types;
using Dolittle.Events;
using Dolittle.Logging;

namespace Dolittle.Runtime.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessors"/>
    /// </summary>
    [Singleton]
    public class EventProcessors : IEventProcessors
    {
        readonly Dictionary<IApplicationArtifactIdentifier, List<IEventProcessor>> _eventProcessorsByResourceIdentifier;
        readonly List<IEventProcessor> _eventProcessors = new List<IEventProcessor>();
        readonly IApplicationArtifacts _applicationResources;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessors"/>
        /// </summary>
        /// <param name="applicationResources"><see cref="IApplicationArtifacts"/> for resolving resources</param>
        /// <param name="systemsThatKnowsAboutEventProcessors">Instances of <see cref="IKnowAboutEventProcessors"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public EventProcessors(IApplicationArtifacts applicationResources, IInstancesOf<IKnowAboutEventProcessors> systemsThatKnowsAboutEventProcessors, ILogger logger)
        {
            _applicationResources = applicationResources;
            _eventProcessorsByResourceIdentifier = GatherEventProcessorsFrom(systemsThatKnowsAboutEventProcessors);
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<IEventProcessor> All => _eventProcessors;

        /// <inheritdoc/>
        public IEventProcessingResults Process(IEventEnvelope envelope, IEvent @event)
        {
            _logger.Trace("Process event");
            var identifier = _applicationResources.Identify(@event);
            _logger.Trace($"Identifier for event - {identifier}");
            if (!_eventProcessorsByResourceIdentifier.ContainsKey(identifier)) {
                _logger.Trace("No event processors able to process - return");
                return new EventProcessingResults(new IEventProcessingResult[0]);
            }

            List<IEventProcessingResult> results = new List<IEventProcessingResult>();
            var eventProcessors = _eventProcessorsByResourceIdentifier[identifier];
            eventProcessors.ForEach(e => {
                _logger.Trace($"Process event with processor : {e.Identifier}");
                results.Add(e.Process(envelope, @event));
            });

            return new EventProcessingResults(results);
        }

        Dictionary<IApplicationArtifactIdentifier, List<IEventProcessor>> GatherEventProcessorsFrom(IEnumerable<IKnowAboutEventProcessors> systemsThatHasEventProcessors)
        {
            var eventProcessorsByResourceIdentifier = new Dictionary<IApplicationArtifactIdentifier, List<IEventProcessor>>();
            systemsThatHasEventProcessors.ForEach(a => a.ForEach(e =>
            {
                List<IEventProcessor> eventProcessors;
                if (eventProcessorsByResourceIdentifier.ContainsKey(e.Event)) eventProcessors = eventProcessorsByResourceIdentifier[e.Event];
                else
                {
                    eventProcessors = new List<IEventProcessor>();
                    eventProcessorsByResourceIdentifier[e.Event] = eventProcessors;
                }
                eventProcessors.Add(e);
            }));

            return eventProcessorsByResourceIdentifier;
        }
    }
}
