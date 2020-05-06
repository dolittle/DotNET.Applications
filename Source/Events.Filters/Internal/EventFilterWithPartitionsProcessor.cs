// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Runtime.Events.Processing.Contracts;
using Dolittle.Services.Clients;
using static Dolittle.Runtime.Events.Processing.Contracts.Filters;

namespace Dolittle.Events.Filters.Internal
{
    /// <summary>
    /// An implementation of <see cref="AbstractFilterProcessor"/> used for <see cref="ICanFilterEventsWithPartition"/>.
    /// </summary>
    public class EventFilterWithPartitionsProcessor : AbstractFilterProcessor
    {
        readonly FactoryFor<IReverseCallClient<PartitionedFiltersClientToRuntimeMessage, FilterRuntimeToClientMessage, PartitionedFiltersRegistrationRequest, FilterRegistrationResponse, FilterEventRequest, PartitionedFilterResponse>> _clientFactory;
        readonly ICanFilterEventsWithPartition _filter;
        IReverseCallClient<PartitionedFiltersClientToRuntimeMessage, FilterRuntimeToClientMessage, PartitionedFiltersRegistrationRequest, FilterRegistrationResponse, FilterEventRequest, PartitionedFilterResponse> _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilterWithPartitionsProcessor"/> class.
        /// </summary>
        /// <param name="filtersClient">The <see cref="FiltersClient"/> to use to connect to the Runtime.</param>
        /// <param name="reverseCallClients">The <see cref="IReverseCallClients"/> to use for creating instances of <see cref="IReverseCallClient{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}"/>.</param>
        /// <param name="filter">The <see cref="ICanFilterEventsWithPartition"/> to use for filtering the events.</param>
        /// <param name="converter">The <see cref="IEventConverter"/> to use to convert events.</param>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        public EventFilterWithPartitionsProcessor(FiltersClient filtersClient, IReverseCallClients reverseCallClients, ICanFilterEventsWithPartition filter, IEventConverter converter, ILogger logger)
            : base(converter, logger)
        {
            _clientFactory = () => reverseCallClients.GetFor<PartitionedFiltersClientToRuntimeMessage, FilterRuntimeToClientMessage, PartitionedFiltersRegistrationRequest, FilterRegistrationResponse, FilterEventRequest, PartitionedFilterResponse>(
                () => filtersClient.ConnectPartitioned(),
                (message, arguments) => message.RegistrationRequest = arguments,
                message => message.RegistrationResponse,
                message => message.FilterRequest,
                (message, response) => message.FilterResult = response,
                (arguments, context) => arguments.CallContext = context,
                request => request.CallContext,
                (response, context) => response.CallContext = context);
            _filter = filter;
        }

        /// <inheritdoc/>
        public override Failure RegisterFailure => _client.ConnectResponse.Failure;

        /// <inheritdoc/>
        public override Task<bool> Register(FilterId filter, ScopeId scope, CancellationToken cancellationToken)
        {
            _client = _clientFactory();
            return _client.Connect(
                new PartitionedFiltersRegistrationRequest
                {
                    FilterId = filter.ToProtobuf(),
                    ScopeId = scope.ToProtobuf(),
                },
                cancellationToken);
        }

        /// <inheritdoc/>
        public override Task Handle(CancellationToken cancellationToken)
            => _client.Handle(Call, cancellationToken);

        async Task<PartitionedFilterResponse> Call(FilterEventRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await InvokeFilter(_filter, request.Event, request.RetryProcessingState, request.ScopeId).ConfigureAwait(false);
                return new PartitionedFilterResponse
                {
                    IsIncluded = result.Included,
                    PartitionId = result.Partition.ToProtobuf(),
                };
            }
            catch (Exception ex)
            {
                return new PartitionedFilterResponse
                {
                    Failure = CreateFailureFrom(_filter, ex, request.RetryProcessingState),
                };
            }
        }
    }
}