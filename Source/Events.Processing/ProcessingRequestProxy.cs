// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using Dolittle.Execution;
using Google.Protobuf;

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
        /// Gets the <see cref="CommittedEvent" />.
        /// </summary>
        public abstract CommittedEvent Event { get; }

        /// <summary>
        /// Gets the <see cref="PartitionId" />.
        /// </summary>
        public abstract PartitionId Partition { get; }

        /// <summary>
        /// Gets the <see cref="ExecutionContext" />.
        /// </summary>
        public abstract ExecutionContext ExecutionContext { get; }

        /// <summary>
        /// Gets the <see typeparam="TRequest" />.
        /// </summary>
        protected TRequest Request { get; }
    }
}