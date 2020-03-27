// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Threading;
using System.Threading.Tasks;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Resilience;
using Dolittle.Services.Clients;
using Grpc.Core;
using static contracts::Dolittle.Runtime.Events.Processing.PublicFilters;

namespace Dolittle.Events.Filters.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="IFilterProcessor" /> that can process <see cref="ICanFilterPublicEvents" /> filters.
    /// </summary>
    public class PublicEventsFilterProcessor : IFilterProcessor
    {
        readonly IEventProcessingInvocationManager _eventProcessingInvocationManager;
        readonly PublicFiltersClient _filtersClient;
        readonly IExecutionContextManager _executionContextManager;
        readonly IEventConverter _eventConverter;
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly IAsyncPolicyFor<PublicEventsFilterProcessor> _policy;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicEventsFilterProcessor"/> class.
        /// </summary>
        /// <param name="eventProcessingInvocationManager">The <see cref="IEventProcessingInvocationManager" />.</param>
        /// <param name="filtersClient"><see cref="PublicFiltersClient"/> for talking to the runtime.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager" />.</param>
        /// <param name="eventConverter"><see cref="IEventConverter"/> for converting events for transport.</param>
        /// <param name="reverseCallClientManager">A <see cref="IReverseCallClientManager"/> for working with reverse calls from server.</param>
        /// <param name="policy">Policy for <see cref="PublicEventsFilterProcessor"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public PublicEventsFilterProcessor(
            IEventProcessingInvocationManager eventProcessingInvocationManager,
            PublicFiltersClient filtersClient,
            IExecutionContextManager executionContextManager,
            IEventConverter eventConverter,
            IReverseCallClientManager reverseCallClientManager,
            IAsyncPolicyFor<PublicEventsFilterProcessor> policy,
            ILogger logger)
        {
            _eventProcessingInvocationManager = eventProcessingInvocationManager;
            _filtersClient = filtersClient;
            _executionContextManager = executionContextManager;
            _eventConverter = eventConverter;
            _reverseCallClientManager = reverseCallClientManager;
            _policy = policy;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanProcess(IEventStreamFilter filter) => typeof(ICanFilterPublicEvents).IsAssignableFrom(filter.GetType());

        /// <inheritdoc/>
        public Task Start(IEventStreamFilter filter, CancellationToken token)
        {
            if (!CanProcess(filter)) throw new FilterProcessorCannotStartProcessingFilter(this, filter);
            var scope = ScopeId.Default;
            var additionalInfo = new FilterArguments
            {
                Filter = filter.Identifier.ToProtobuf(),
                Scope = scope.ToProtobuf()
            };
            var metadata = new Metadata { additionalInfo.ToArgumentsMetadata() };

            _logger.Debug($"Connecting to runtime for public filter '{filter.Identifier}' on source stream '{filter.SourceStreamId}'");

            var result = _filtersClient.Connect(metadata);
            return _reverseCallClientManager.Handle(
                result,
                _ => _.CallNumber,
                _ => _.CallNumber,
                async call =>
                {
                    var partition = call.Request.Partition.To<PartitionId>();
                    var executionContext = Execution.Contracts.ExecutionContext.Parser.ParseFrom(call.Request.ExecutionContext);
                    _executionContextManager.CurrentFor(executionContext);
                    var correlationId = executionContext.CorrelationId.To<CorrelationId>();

                    var committedEvent = _eventConverter.ToSDK(call.Request.Event);
                    var invocationManager = _eventProcessingInvocationManager.GetInvocationManagerFor<PublicFilterClientToRuntimeResponse, IFilterResult>();
                    var response = await invocationManager.Invoke(
                        async () =>
                        {
                            var filterResult = await filter.Filter(committedEvent).ConfigureAwait(false);
                            return new SucceededFilteringResult(filterResult.IsIncluded, filterResult.Partition);
                        },
                        filter.GetType(),
                        partition,
                        call.Request.RetryProcessingState,
                        succeededFilterResult => new PublicFilterClientToRuntimeResponse
                            {
                                Success = new SuccessfulFilter
                                    {
                                        IsIncluded = succeededFilterResult.IsIncluded,
                                        Partition = succeededFilterResult.Partition.ToProtobuf()
                                    }
                            },
                        response => response.Failed).ConfigureAwait(false);
                    await _policy.Execute(() => call.Reply(response)).ConfigureAwait(false);
                }, token);
        }
    }
}