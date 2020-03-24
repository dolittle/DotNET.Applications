// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using Google.Protobuf;
using grpcEvents = contracts::Dolittle.Runtime.Events;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents the proxy object for a event processing request.
    /// </summary>
    /// <typeparam name="TRequest">The request <see cref="IMessage" /> type.</typeparam>
    public abstract class ProcessingRequestProxy<TRequest>
        where TRequest : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingRequestProxy{T}"/> class.
        /// </summary>
        /// <param name="request">The processing request.</param>
        protected ProcessingRequestProxy(TRequest request)
        {
            Request = request;
        }

        /// <summary>
        /// Gets the <see cref="grpcEvents.CommittedEvent" />.
        /// </summary>
        public abstract grpcEvents.CommittedEvent Event { get; }

        /// <summary>
        /// Gets the <see cref="PartitionId" />.
        /// </summary>
        public abstract PartitionId Partition { get; }

        /// <summary>
        /// Gets the <see cref="Execution.Contracts.ExecutionContext" />.
        /// </summary>
        public abstract Execution.Contracts.ExecutionContext ExecutionContext { get; }

        /// <summary>
        /// Gets the <see typeparam="TRequest" />.
        /// </summary>
        public TRequest Request { get; }
    }
}