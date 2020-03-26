// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Heads;
using Dolittle.Logging;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Performs the boot procedure for the processing of events.
    /// </summary>
    public class HeadConnectionLifecycle : ITakePartInHeadConnectionLifecycle
    {
        readonly ISubscriptionsClient _subscriptionsClient;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadConnectionLifecycle"/> class.
        /// </summary>
        /// <param name="subscriptionsClient">The <see cref="ISubscriptionsClient" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public HeadConnectionLifecycle(
            ISubscriptionsClient subscriptionsClient,
            ILogger logger)
        {
            _logger = logger;
            _subscriptionsClient = subscriptionsClient;
        }

        /// <inheritdoc/>
        public bool IsReady() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public Task OnConnected(CancellationToken token = default)
        {
            _subscriptionsClient.Subscribe();
            return new TaskCompletionSource<bool>().Task;
        }

        /// <inheritdoc/>
        public void OnDisconnected()
        {
        }
    }
}