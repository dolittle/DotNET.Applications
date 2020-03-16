// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
extern alias contracts;

using contracts::Dolittle.Runtime.EventHorizon;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using grpc = contracts::Dolittle.Runtime.EventHorizon;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="ISubscriptionsClient" />.
    /// </summary>
    [Singleton]
    public class SubscriptionsClient : ISubscriptionsClient
    {
        readonly EventHorizonsConfiguration _eventHorizons;
        readonly Application _application;
        readonly Microservice _microservice;
        readonly grpc.Subscriptions.SubscriptionsClient _client;
        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsClient"/> class.
        /// </summary>
        /// <param name="eventHorizons">The <see cref="EventHorizonsConfiguration" />.</param>
        /// <param name="boundedContextConfiguration">The <see cref="BoundedContextConfiguration" />.</param>
        /// <param name="subscriptionsClient">The <see cref="grpc.Subscriptions.SubscriptionsClient" />.</param>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public SubscriptionsClient(
            EventHorizonsConfiguration eventHorizons,
            BoundedContextConfiguration boundedContextConfiguration,
            grpc.Subscriptions.SubscriptionsClient subscriptionsClient,
            IExecutionContextManager executionContextManager,
            ILogger logger)
        {
            _eventHorizons = eventHorizons;
            _application = boundedContextConfiguration.Application;
            _microservice = boundedContextConfiguration.BoundedContext;
            _client = subscriptionsClient;
            _executionContextManager = executionContextManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Subscribe()
        {
            foreach ((var subscriber, var eventHorizon) in _eventHorizons)
            {
                eventHorizon.ForEach(_ =>
                {
                    var request = new Subscription
                    {
                        Microservice = _.Microservice.ToProtobuf(),
                        Tenant = _.Tenant.ToProtobuf()
                    };
                    _executionContextManager.CurrentFor(
                        _application,
                        _microservice.Value,
                        subscriber);
                    var response = _client.Subscribe(request);
                    if (!response.Success) throw new FailedToSubscribeToEventHorizon(subscriber, _.Microservice, _.Tenant);
                });
            }
        }
    }
}