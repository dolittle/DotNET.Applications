// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events;

namespace Dolittle.Domain
{
    /// <summary>
    /// Defines the very basic functionality needed for an aggregated root.
    /// </summary>
    public interface IAggregateRoot : IEventSource
    {
    }
}