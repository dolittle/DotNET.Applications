// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.Tenancy;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Defines a system for subscribing to event horizons.
    /// </summary>
    public interface ISubscriptionsClient
    {
        /// <summary>
        /// Notifies the runtime to subscribe to an event horizon.
        /// </summary>
        /// <param name="consumerTenant">The consumer <see cref="TenantId" />.</param>
        /// <param name="eventHorizon">The <see cref="EventHorizon" />.</param>
        /// <returns>The asynchronous operation.</returns>
        Task Subscribe(TenantId consumerTenant, EventHorizon eventHorizon);
    }
}