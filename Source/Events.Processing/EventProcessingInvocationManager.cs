// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Lifecycle;
using Dolittle.Logging;
using Google.Protobuf;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingInvocationManager" />.
    /// </summary>
    [Singleton]
    public class EventProcessingInvocationManager : IEventProcessingInvocationManager
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessingInvocationManager"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public EventProcessingInvocationManager(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult> GetInvocationManagerFor<TProcessingResponse, TProcessingResult>()
            where TProcessingResponse : IMessage, new()
            where TProcessingResult : IProcessingResult => new EventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult>(_logger);
    }
}