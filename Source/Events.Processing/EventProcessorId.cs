// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents a unique identifier for an event stream processor.
    /// </summary>
    public class EventProcessorId : Value<EventProcessorId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorId"/> class.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="sourceStream">The source <see cref="StreamId "/>.</param>
        /// <param name="processor">The identifier of the processor.</param>
        public EventProcessorId(ScopeId scope, StreamId sourceStream, Guid processor)
        {
            Scope = scope;
            SourceStream = sourceStream;
            Processor = processor;
        }

        /// <summary>
        /// Gets the <see cref="ScopeId" />.
        /// </summary>
        public ScopeId Scope { get; }

        /// <summary>
        /// Gets the <see cref="StreamId" />.
        /// </summary>
        public StreamId SourceStream { get; }

        /// <summary>
        /// Gets the id of the processor.
        /// </summary>
        public Guid Processor { get; }
    }
}