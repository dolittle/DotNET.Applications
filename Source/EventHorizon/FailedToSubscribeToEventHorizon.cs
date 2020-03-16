// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Applications;
using Dolittle.Tenancy;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Exception that gets thrown when the subscription to an event horizon fails.
    /// </summary>
    public class FailedToSubscribeToEventHorizon : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToSubscribeToEventHorizon"/> class.
        /// </summary>
        /// <param name="subscriber">The subscriber <see cref="TenantId" />.</param>
        /// <param name="microservice">The <see cref="Microservice" /> to subscribe to.</param>
        /// <param name="producerTenant">The <see cref="TenantId" /> to subscribe to.</param>
        public FailedToSubscribeToEventHorizon(TenantId subscriber, Microservice microservice, TenantId producerTenant)
            : base($"Tenant '{subscriber}' could not subscribe to event horizon for tenant '{producerTenant}' on microservice '{microservice}'")
        {
        }
    }
}