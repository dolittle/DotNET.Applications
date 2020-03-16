// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;
using Dolittle.Logging;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Performs the boot procedure for the processing of events.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly ISubscriptionsClient _subscriptionsClient;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="subscriptionsClient">The <see cref="ISubscriptionsClient" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public BootProcedure(
            ISubscriptionsClient subscriptionsClient,
            ILogger logger)
        {
            _logger = logger;
            _subscriptionsClient = subscriptionsClient;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _subscriptionsClient.Subscribe();
        }
    }
}