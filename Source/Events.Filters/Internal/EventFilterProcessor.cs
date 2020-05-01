// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Runtime.Events.Processing.Contracts;
using Dolittle.Services.Clients;
using static Dolittle.Runtime.Events.Processing.Contracts.Filters;

namespace Dolittle.Events.Filters.Internal
{
    /// <summary>
    /// An implementation of <see cref="AbstractFilterProcessor"/> used for <see cref="ICanFilterEvents"/>.
    /// </summary>
    public class EventFilterProcessor : AbstractFilterProcessor
    {
        readonly IReverseCallClient<FiltersClientToRuntimeMessage, FilterRuntimeToClientMessage, FiltersRegistrationRequest, FilterRegistrationResponse, FilterEventRequest, FilterResponse> _client;
        readonly ICanFilterEvents _filter;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilterProcessor"/> class.
        /// </summary>
        /// <param name="filtersClient">The <see cref="FiltersClient"/> to use to connect to the Runtime.</param>
        /// <param name="reverseCallClients">The <see cref="IReverseCallClients"/> to use for creating instances of <see cref="IReverseCallClient{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}"/>.</param>
        /// <param name="filter">The <see cref="ICanFilterEvents"/> to use for filtering the events.</param>
        /// <param name="converter">The <see cref="IEventConverter"/> to use to convert events.</param>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        public EventFilterProcessor(FiltersClient filtersClient, IReverseCallClients reverseCallClients, ICanFilterEvents filter, IEventConverter converter, ILogger logger)
            : base(converter, logger)
        {
            _client = reverseCallClients.GetFor<FiltersClientToRuntimeMessage, FilterRuntimeToClientMessage, FiltersRegistrationRequest, FilterRegistrationResponse, FilterEventRequest, FilterResponse>(
                () => filtersClient.Connect(),
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
            => _client.Connect(
                new FiltersRegistrationRequest
                {
                    FilterId = filter.ToProtobuf(),
                    ScopeId = scope.ToProtobuf(),
                },
                cancellationToken);

        /// <inheritdoc/>
        public override Task Handle(CancellationToken cancellationToken)
            => _client.Handle(Call, cancellationToken);

        async Task<FilterResponse> Call(FilterEventRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await InvokeFilter(_filter, request.Event, request.RetryProcessingState, request.ScopeId).ConfigureAwait(false);
                return new FilterResponse
                {
                    IsIncluded = result.Included,
                };
            }
            catch (Exception ex)
            {
                return new FilterResponse
                {
                    Failure = CreateFailureFrom(_filter, ex, request.RetryProcessingState),
                };
            }
        }
    }
}