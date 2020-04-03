// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Heads;
using Dolittle.Logging;
using Dolittle.Resilience;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Performs the boot procedure for the processing of events.
    /// </summary>
    public class HeadConnectionLifecycle : ITakePartInHeadConnectionLifecycle
    {
        readonly ISubscriptionsClient _subscriptionsClient;
        readonly EventHorizonsConfiguration _eventHorizons;
        readonly IAsyncPolicyFor<HeadConnectionLifecycle> _policy;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadConnectionLifecycle"/> class.
        /// </summary>
        /// <param name="subscriptionsClient">The <see cref="ISubscriptionsClient" />.</param>
        /// <param name="eventHorizons">The <see cref="EventHorizonsConfiguration" />.</param>
        /// <param name="policy">The <see cref="IAsyncPolicyFor{T}" /> <see cref="HeadConnectionLifecycle" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public HeadConnectionLifecycle(
            ISubscriptionsClient subscriptionsClient,
            EventHorizonsConfiguration eventHorizons,
            IAsyncPolicyFor<HeadConnectionLifecycle> policy,
            ILogger logger)
        {
            _logger = logger;
            _eventHorizons = eventHorizons;
            _policy = policy;
            _subscriptionsClient = subscriptionsClient;
        }

        /// <inheritdoc/>
        public bool IsReady() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public async Task OnConnected(CancellationToken token = default)
        {
            _logger.Debug($"Subscribing to event horizons based on '{EventHorizonsConfiguration.ConfigurationName}' configuration.");
            var tasks = _eventHorizons.SelectMany(_ =>
                {
                    var consumerTenant = _.Key;
                    var eventHorizons = _.Value;
                    return eventHorizons.Select(eventHorizon => _policy.Execute((token) => _subscriptionsClient.Subscribe(consumerTenant, eventHorizon, token), token));
                });
            if (!tasks.Any()) return;
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}