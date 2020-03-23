// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Applications;

namespace Dolittle.Events.EventHorizon
{
    /// <summary>
    /// Exception that gets thrown when the producer <see cref="Microservice" /> id is not set.
    /// </summary>
    public class ProducerMicroserviceIdMustBeSet : IllegalProducerMicroserviceId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerMicroserviceIdMustBeSet"/> class.
        /// </summary>
        /// <param name="microserviceId">The illegal <see cref="Microservice" />.</param>
        public ProducerMicroserviceIdMustBeSet(Microservice microserviceId)
            : base(microserviceId, $"The producer microservice id cannot be '{Microservice.NotSet}'")
        {
        }
    }
}