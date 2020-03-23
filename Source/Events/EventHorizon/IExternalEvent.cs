// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.EventHorizon
{
    /// <summary>
    /// Defines the basics of an external event.
    /// </summary>
    /// <remarks>
    /// This is a marker interface to mark events that are coming from an external system.
    /// </remarks>
    public interface IExternalEvent : IEvent
    {
    }
}
