// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Events.Processing;
using Dolittle.Protobuf;
using grpcEvents = contracts::Dolittle.Runtime.Events;

namespace Dolittle.Events.Filters.EventHorizon
{
    /// <summary>
    /// Represents the <see cref="ProcessingRequestProxy{TRequest}" /> for <see cref="PublicFilterRuntimeToClientRequest" />.
    /// </summary>
    public class PublicEventsProcessingRequestProxy : ProcessingRequestProxy<PublicFilterRuntimeToClientRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicEventsProcessingRequestProxy"/> class.
        /// </summary>
        /// <param name="request">The <see cref="PublicFilterRuntimeToClientRequest" />.</param>
        public PublicEventsProcessingRequestProxy(PublicFilterRuntimeToClientRequest request)
            : base(request)
        {
            Event = request.Event;
            Partition = request.Partition.To<PartitionId>();
            ExecutionContext = Execution.Contracts.ExecutionContext.Parser.ParseFrom(request.ExecutionContext);
        }

        /// <inheritdoc/>
        public override grpcEvents.CommittedEvent Event { get; }

        /// <inheritdoc/>
        public override PartitionId Partition { get; }

        /// <inheritdoc/>
        public override Execution.Contracts.ExecutionContext ExecutionContext { get; }
    }
}