// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Events.EventHorizon
{
    /// <summary>
    /// Reprsents the reason for why the producer microservice id is illegal.
    /// </summary>
    public class IllegalProducerMicroserviceIdReason : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly converts the <see cref="string" /> to a <see cref="IllegalProducerMicroserviceIdReason" />.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public static implicit operator IllegalProducerMicroserviceIdReason(string reason) => new IllegalProducerMicroserviceIdReason { Value = reason };
    }
}