// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// A method is marked as an EventProcessor but does not have the correct method signature.
    /// </summary>
    /// <remarks>
    /// void [MethodName](MyEvent @event)
    /// void [MethodName](MyEvent @event, EventSourceId id)
    /// void [MethodName](MyEvent @event, EventMetadata eventMetadata)
    /// .
    /// </remarks>
    public class EventProcessorMethodParameterMismatch : Exception
    {
        const string ErrorMessage = @"
            An event processor method must conform to one of the following method signtaures:
            void [MethodName](MyEvent @event)
            void [MethodName](MyEvent @event, EventSourceId id)
            void [MethodName](MyEvent @event, EventMetadata eventMetadata)
        ";

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorMethodParameterMismatch"/> class.
        /// </summary>
        public EventProcessorMethodParameterMismatch()
            : base(ErrorMessage)
        {
        }
    }
}
