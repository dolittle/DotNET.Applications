// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Google.Protobuf;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that knows how to hand the processing of event streams.
    /// </summary>
    public interface IEventStreamProcessors
    {
        /// <summary>
        /// Starts the processing of a stream of events.
        /// </summary>
        /// <typeparam name="TResponse">The response <see cref="IMessage" /> type.</typeparam>
        /// <typeparam name="TRequest">The request <see cref="IMessage" /> type.</typeparam>
        /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" /> type.</typeparam>
        /// <param name="eventProcessor">The <see cref="EventProcessor{TRequest, TResponse, TProcessingResult}" /> stream.</param>
        void StartProcessing<TResponse, TRequest, TProcessingResult>(EventProcessor<TResponse, TRequest, TProcessingResult> eventProcessor)
            where TResponse : IMessage
            where TRequest : IMessage
            where TProcessingResult : IProcessingResult;
    }
}