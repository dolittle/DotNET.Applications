// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Serialization.Json;
using Dolittle.Services.Clients;
using Grpc.Core;
using static contracts::Dolittle.Runtime.Events.Processing.Filters;
using grpc = contracts::Dolittle.Runtime.Events.Processing;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents an implementation of <see cref="IFilterProcessor" /> that can process <see cref="ICanFilterPrivateEvents" /> filters.
    /// </summary>
    public class PrivateEventsFilterProcessor : IFilterProcessor
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IEventConverter _eventConverter;
        readonly ISerializer _serializer;
        readonly FiltersClient _filtersClient;
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateEventsFilterProcessor"/> class.
        /// </summary>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for managing the <see cref="ExecutionContext"/>.</param>
        /// <param name="eventConverter"><see cref="IEventConverter" /> for converting events.</param>
        /// <param name="serializer"><see cref="ISerializer"/> for serialization.</param>
        /// <param name="filtersClient"><see cref="FiltersClient"/> for connecting to server.</param>
        /// <param name="reverseCallClientManager">A <see cref="IReverseCallClientManager"/> for working with reverse calls from server.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public PrivateEventsFilterProcessor(
            IExecutionContextManager executionContextManager,
            IEventConverter eventConverter,
            ISerializer serializer,
            FiltersClient filtersClient,
            IReverseCallClientManager reverseCallClientManager,
            ILogger logger)
        {
            _executionContextManager = executionContextManager;
            _eventConverter = eventConverter;
            _serializer = serializer;
            _filtersClient = filtersClient;
            _reverseCallClientManager = reverseCallClientManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanProcess(IEventStreamFilter filter) => typeof(ICanFilterPrivateEvents).IsAssignableFrom(filter.GetType());

        /// <inheritdoc/>
        public void Start(IEventStreamFilter filter)
        {
            if (!CanProcess(filter)) throw new FilterProcessorCannotStartProcessingFilter(this, filter);
            _logger.Information($"Starting processor for private filter with identifier '{filter.Identifier}' on source stream '{filter.SourceStreamId}'");

            var additionalInfo = new FilterArguments
            {
                Filter = filter.Identifier.ToProtobuf()
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

                        var committedEvent = _eventConverter.ToSDK(call.Request.Event);

                        _logger.Information($"Event @ {committedEvent.Occurred} - {committedEvent.Event.GetType().AssemblyQualifiedName} - {_serializer.EventToJson(committedEvent.Event)} received for filtering");

                        var filterResult = await filter.Filter(committedEvent).ConfigureAwait(false);
                        var response = new grpc.FilterClientToRuntimeResponse
                        {
                            Succeeded = true,
                            Retry = false,
                            IsIncluded = filterResult.IsIncluded,
                            Partition = filterResult.Partition.Value.ToProtobuf(),
                            ExecutionContext = call.Request.ExecutionContext
                        };
                        await call.Reply(response).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        var response = new grpc.FilterClientToRuntimeResponse
                        {
                            Succeeded = false,
                            Retry = false,
                            FailureReason = $"Failure Message: {ex.Message}\nStack Trace: {ex.StackTrace}",
                            ExecutionContext = call.Request.ExecutionContext
                        };
                        await call.Reply(response).ConfigureAwait(false);
                        _logger.Error(ex, "Error handling event in filter");
                    }
                });
        }
    }
}