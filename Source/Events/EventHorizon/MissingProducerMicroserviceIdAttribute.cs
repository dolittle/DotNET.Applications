// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.EventHorizon
{
    /// <summary>
    /// Exception that gets thrown when the producer microservice id is an illegal value.
    /// </summary>
    public class MissingProducerMicroserviceIdAttribute : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingProducerMicroserviceIdAttribute"/> class.
        /// </summary>
        /// <param name="eventType">The <see cref="Type" /> of the <see cref="IExternalEvent" />.</param>
        public MissingProducerMicroserviceIdAttribute(Type eventType)
            : base($"The external event '{eventType.FullName}' is missing the [ProducerMicroservice(\"{Guid.Empty}\")] attribute.")
        {
        }
    }
}