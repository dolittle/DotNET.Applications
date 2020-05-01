// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Events.Filters.EventHorizon;
using Dolittle.Events.Filters.Internal;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Resilience;
using Dolittle.Services.Clients;
using static Dolittle.Runtime.Events.Processing.Contracts.Filters;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// An implementation of <see cref="IFilterManager"/>.
    /// </summary>
    public class FilterManager : IFilterManager
    {
        readonly FiltersClient _filterClient;
        readonly IReverseCallClients _reverseCallClients;
        readonly IEventConverter _converter;
        readonly IAsyncPolicyFor<FilterManager> _policy;
        readonly ILoggerManager _loggerManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterManager"/> class.
        /// </summary>
        /// <param name="filterClient">The <see cref="FiltersClient"/> used to connect to the Runtime.</param>
        /// <param name="reverseCallClients">The <see cref="IReverseCallClients"/> used to construct instances of <see cref="IReverseCallClient{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}"/>.</param>
        /// <param name="converter">The <see cref="IEventConverter"/> to use to convert events.</param>
        /// <param name="policy">The <see cref="IAsyncPolicyFor{T}"/> that defines reconnect policies for the event filters.</param>
        /// <param name="loggerManager">The <see cref="ILoggerManager"/> used to create instances of <see cref="ILogger"/> for the filter processors.</param>
        /// <param name="logger">The <see cref="ILogger"/> used for logging.</param>
        public FilterManager(
            FiltersClient filterClient,
            IReverseCallClients reverseCallClients,
            IEventConverter converter,
            IAsyncPolicyFor<FilterManager> policy,
            ILoggerManager loggerManager,
            ILogger logger)
        {
            _filterClient = filterClient;
            _reverseCallClients = reverseCallClients;
            _converter = converter;
            _policy = policy;
            _loggerManager = loggerManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task Register<TEventType, TFilterResult>(FilterId id, ScopeId scope, ICanFilter<TEventType, TFilterResult> filter, CancellationToken cancellationToken)
            where TEventType : IEvent
        {
            AbstractFilterProcessor processor;
            switch (filter)
            {
                case ICanFilterEvents eventFilter:
                processor = new EventFilterProcessor(_filterClient, _reverseCallClients, eventFilter, _converter, _loggerManager.CreateLogger<EventFilterProcessor>());
                break;

                case ICanFilterEventsWithPartition eventFilter:
                processor = new EventFilterWithPartitionsProcessor(_filterClient, _reverseCallClients, eventFilter, _converter, _loggerManager.CreateLogger<EventFilterWithPartitionsProcessor>());
                break;

                case ICanFilterPublicEvents eventFilter:
                processor = new PublicEventFilterProcessor(_filterClient, _reverseCallClients, eventFilter, _converter, _loggerManager.CreateLogger<PublicEventFilterProcessor>());
                break;

                default:
                _logger.Warning("Unknown filter type {Type}", filter.GetType());
                return Task.CompletedTask;
            }

            return Task.Run(() => Start(id, scope, processor, cancellationToken), cancellationToken);
        }

        Task Start(FilterId id, ScopeId scope, AbstractFilterProcessor processor, CancellationToken cancellationToken)
            => _policy.Execute(
                async (cancellationToken) =>
                {
                    var receivedResponse = await processor.Register(id, scope, cancellationToken).ConfigureAwait(false);
                    ThrowIfNotReceivedResponse(id, receivedResponse);
                    ThrowIfRegisterFailure(id, processor.RegisterFailure);
                    await processor.Handle(cancellationToken).ConfigureAwait(false);
                    ThrowFilterShouldNeverComplete(id);
                },
                cancellationToken);

        void ThrowIfNotReceivedResponse(FilterId id, bool receivedResponse)
        {
            if (!receivedResponse) throw new DidNotReceiveFilterRegistrationResponse(id);
        }

        void ThrowIfRegisterFailure(FilterId id, Failure registerFailure)
        {
            if (registerFailure != null) throw new FilterRegistrationFailed(id, registerFailure);
        }

        void ThrowFilterShouldNeverComplete(FilterId id)
        {
            throw new FilterShouldNeverComplete(id);
        }
    }
}