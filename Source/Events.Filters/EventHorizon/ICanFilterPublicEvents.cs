// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Filters.EventHorizon
{
    /// <summary>
    /// Defines a system that can filter events to a public stream.
    /// </summary>
    public interface ICanFilterPublicEvents : IEventStreamFilter
    {
    }
}