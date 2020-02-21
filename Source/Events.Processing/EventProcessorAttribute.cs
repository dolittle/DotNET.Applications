// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Decorates a method to indicate that the method is an Event Processor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EventProcessorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorAttribute"/> class.
        /// </summary>
        /// <param name="id">The <see cref="Guid" /> identifier.</param>
        public EventProcessorAttribute(string id)
        {
            Id = Guid.Parse(id);
            ThrowIfIllegalEventProcessorId(Id);
        }

        /// <summary>
        /// Gets the unique id for this event processor.
        /// </summary>
        public EventProcessorId Id { get; }

        void ThrowIfIllegalEventProcessorId(EventProcessorId id)
        {
            var stream = new StreamId { Value = id };
            if (stream.IsNonWriteable) throw new IllegalEventProcessorId(id);
        }
    }
}