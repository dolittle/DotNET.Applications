/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Events;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Runtime.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessors"/>
    /// </summary>
    [Singleton]
    public class EventProcessors : IEventProcessors
    {
        readonly Dictionary<Artifact, List<IEventProcessor>> _eventProcessorsByResourceIdentifier;
        readonly List<IEventProcessor> _eventProcessors = new List<IEventProcessor>();
        readonly IArtifactTypeMap _artifactTypeMap;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessors"/>
        /// </summary>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for resolving resources</param>
        /// <param name="systemsThatKnowsAboutEventProcessors">Instances of <see cref="IKnowAboutEventProcessors"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public EventProcessors(IArtifactTypeMap artifactTypeMap, IInstancesOf<IKnowAboutEventProcessors> systemsThatKnowsAboutEventProcessors, ILogger logger)
        {
            _artifactTypeMap = artifactTypeMap;
            _eventProcessorsByResourceIdentifier = GatherEventProcessorsFrom(systemsThatKnowsAboutEventProcessors);
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<IEventProcessor> All => _eventProcessors;

        /// <inheritdoc/>
        public IEventProcessingResults Process(IEventEnvelope envelope, IEvent @event)
        {
            _logger.Trace("Process event");
            var identifier = _artifactTypeMap.GetArtifactFor(@event.GetType());
            _logger.Trace($"Identifier for event - {identifier}");
            if (!_eventProcessorsByResourceIdentifier.ContainsKey(identifier))
            {
                _logger.Trace("No event processors able to process - return");
                return new EventProcessingResults(new IEventProcessingResult[0]);
            }

            List<IEventProcessingResult> results = new List<IEventProcessingResult>();
            var eventProcessors = _eventProcessorsByResourceIdentifier[identifier];
            eventProcessors.ForEach(e =>
            {
                _logger.Trace($"Process event with processor : {e.Identifier}");
                results.Add(e.Process(envelope, @event));
            });

            return new EventProcessingResults(results);
        }

        Dictionary<Artifact, List<IEventProcessor>> GatherEventProcessorsFrom(IEnumerable<IKnowAboutEventProcessors> systemsThatHasEventProcessors)
        {
            var eventProcessorsByArtifact = new Dictionary<Artifact, List<IEventProcessor>>();
            systemsThatHasEventProcessors.ForEach(a => a.ForEach(e =>
            {
                List<IEventProcessor> eventProcessors;
                if (eventProcessorsByArtifact.ContainsKey(e.Event)) eventProcessors = eventProcessorsByArtifact[e.Event];
                else
                {
                    eventProcessors = new List<IEventProcessor>();
                    eventProcessorsByArtifact[e.Event] = eventProcessors;
                }
                eventProcessors.Add(e);
            }));

            return eventProcessorsByArtifact;
        }
    }
}