// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Collections.Concurrent;
using System.Linq;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Serialization.Json;
using Dolittle.Services.Clients;
using Dolittle.Types;
using Grpc.Core;
using static contracts::Dolittle.Runtime.Events.Processing.Filters;
using grpc = contracts::Dolittle.Runtime.Events.Processing;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IStreamFilters"/>.
    /// </summary>
    public class StreamFilters : IStreamFilters
    {
        readonly IInstancesOf<ICanProvideStreamFilters> _filterProviders;
        readonly IExecutionContextManager _executionContextManager;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ISerializer _serializer;
        readonly FiltersClient _filtersClient;
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly ILogger _logger;
        readonly ConcurrentDictionary<Type, ICanFilterEventsInStream> _filters = new ConcurrentDictionary<Type, ICanFilterEventsInStream>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamFilters"/> class.
        /// </summary>
        /// <param name="filterProviders"><see cref="IInstancesOf{T}"/> of <see cref="ICanProvideStreamFilters">providers</see>.</param>
        /// <param name="filters"><see cref="IInstancesOf{T}"/> of <see cref="ICanFilterEventsInStream"/>.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for managing the <see cref="Execution.ExecutionContext"/>.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for getting types from artifacts.</param>
        /// <param name="serializer"><see cref="ISerializer"/> for serialization.</param>
        /// <param name="filtersClient"><see cref="FiltersClient"/> for connecting to server.</param>
        /// <param name="reverseCallClientManager">A <see cref="IReverseCallClientManager"/> for working with reverse calls from server.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public StreamFilters(
            IInstancesOf<ICanProvideStreamFilters> filterProviders,
            IInstancesOf<ICanFilterEventsInStream> filters,
            IExecutionContextManager executionContextManager,
            IArtifactTypeMap artifactTypeMap,
            ISerializer serializer,
            FiltersClient filtersClient,
            IReverseCallClientManager reverseCallClientManager,
            ILogger logger)
        {
            _filterProviders = filterProviders;
            _executionContextManager = executionContextManager;
            _artifactTypeMap = artifactTypeMap;
            _serializer = serializer;
            _filtersClient = filtersClient;
            _reverseCallClientManager = reverseCallClientManager;
            _logger = logger;

            _filterProviders.ForEach(provider => provider.Provide().ForEach(filter => _filters.TryAdd(filter.GetType(), filter)));
            filters.Where(filter => !_filters.ContainsKey(filter.GetType())).ForEach(filter => _filters.TryAdd(filter.GetType(), filter));
        }

        /// <inheritdoc/>
        public void Register()
        {
            _filters.Values.ForEach(filter =>
            {
                _logger.Information($"Registering filter with identifier '{filter.FilterId}' for Stream with identifier '{filter.StreamId}'");

                var additionalInfo = new FilterArguments
                {
                    FilterId = filter.FilterId.ToProtobuf(),
                    StreamId = filter.StreamId.ToProtobuf()
                };
                var metadata = new Metadata { additionalInfo.ToArgumentsMetadata() };

                var result = _filtersClient.Connect(metadata);
                _reverseCallClientManager.Handle(
                    result,
                    _ => _.CallNumber,
                    _ => _.CallNumber,
                    async (call) =>
                    {
                        try
                        {
                            var executionContext = Execution.Contracts.ExecutionContext.Parser.ParseFrom(call.Request.ExecutionContext);
                            _executionContextManager.CurrentFor(executionContext);

                            var artifactId = call.Request.Event.Type.Id.To<ArtifactId>();
                            var artifact = new Artifact(artifactId, call.Request.Event.Type.Generation);
                            var type = _artifactTypeMap.GetTypeFor(artifact);
                            var eventInstance = _serializer.JsonToEvent(type, call.Request.Event.Content);

                            var occurred = call.Request.Event.Occurred.ToDateTimeOffset();

                            _logger.Information($"Event @ {occurred} -  {eventInstance} - {_serializer.EventToJson((IEvent)eventInstance)} received for filtering");

                            var response = new grpc.FilterClientToRuntimeResponse();
                            response.IsIncluded = true;
                            response.ExecutionContext = call.Request.ExecutionContext;
                            await call.Reply(response).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "Error handling event in filter");
                        }
                    });
            });
        }
    }
}