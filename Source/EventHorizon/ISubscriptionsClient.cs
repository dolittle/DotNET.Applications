// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Defines a system for subscribing to event horizons.
    /// </summary>
    public interface ISubscriptionsClient
    {
        /// <summary>
        /// Subscribes based on configurations.
        /// </summary>
        void Subscribe();
    }
}