// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.Logging;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessorInvoker" />.
    /// </summary>
    public class EventProcessorInvoker : IEventProcessorInvoker
    {
        readonly IRetryStrategies _retryStrategies;
        readonly IEventProcessorPartitionContexts _contexts;
        readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retryStrategies">The <see cref="IRetryStrategies" />.</param>
        /// <param name="contexts">The <see cref="IEventProcessorPartitionContexts" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public EventProcessorInvoker(
            IRetryStrategies retryStrategies,
            IEventProcessorPartitionContexts contexts,
            ILogger logger)
        {
            _retryStrategies = retryStrategies;
            _contexts = contexts;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ProcessingInvocationResult> Invoke(ICanProcessEvent processor, CommittedEvent @event, PartitionId partition)
        {
            var retryStrategy = _retryStrategies.GetStrategyFor(processor);
            var processingResult = await processor.Process(@event, partition).ConfigureAwait(false);
        }
    }
}