// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
extern alias contracts;

using System.Threading.Tasks;
using contracts::Dolittle.Runtime.EventHorizon;
using Dolittle.ApplicationModel;
using Dolittle.Applications.Configuration;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Tenancy;
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
        public async Task Subscribe(TenantId consumerTenant, EventHorizon eventHorizon)
        {
            _logger.Trace($"Tenant '{consumerTenant}' subscribing for scope '{eventHorizon.Scope}' to partition '{eventHorizon.Partition}' in stream '{eventHorizon.Stream}' for tenant '{eventHorizon.Tenant}' in microservice '{eventHorizon.Microservice}'");
            var request = new Subscription
            {
                Microservice = eventHorizon.Microservice.ToProtobuf(),
                Tenant = eventHorizon.Tenant.ToProtobuf(),
                Scope = eventHorizon.Scope.ToProtobuf(),
                Stream = eventHorizon.Stream.ToProtobuf(),
                Partition = eventHorizon.Partition.ToProtobuf()
            };
            _executionContextManager.CurrentFor(
                _application,
                _microservice,
                consumerTenant);
            var response = await _client.SubscribeAsync(request);
            _logger.Debug($"Tenant '{consumerTenant}' {(response.Failure == null ? "successfully" : "unsuccessfully")} subscribed for scope '{eventHorizon.Scope}' to partition '{eventHorizon.Partition}' in stream '{eventHorizon.Stream}' for tenant '{eventHorizon.Tenant}' in microservice '{eventHorizon.Microservice}'");
            if (response.Failure != null)
            {
                throw new FailedToSubscribeToEventHorizon(response.Failure.Reason, consumerTenant, eventHorizon.Microservice, eventHorizon.Tenant, eventHorizon.Stream, eventHorizon.Partition);
            }
        }
    }
}