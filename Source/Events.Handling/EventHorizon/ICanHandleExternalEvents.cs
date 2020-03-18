// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Defines a system that can handle <see cref="IExternalEvent" /> events from other microservices.
    /// </summary>
    /// <remarks>
    /// An implementation must then implement a Handle method that takes the
    /// specific <see cref="IExternalEvent">event</see> you want to handle.
    /// </remarks>
    public interface ICanHandleExternalEvents
    {
    }
}