// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents the base class used for aggregated roots in your domain.
    /// </summary>
    public class AggregateRoot : EventSource, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        /// <param name="id"><see cref="EventSourceId"/> of the AggregatedRoot.</param>
        /// <remarks>
        /// An <see cref="AggregateRoot"/> is a type of <see cref="IEventSource"/>.
        /// </remarks>
        protected AggregateRoot(EventSourceId id)
            : base(id)
        {
        }
    }
}