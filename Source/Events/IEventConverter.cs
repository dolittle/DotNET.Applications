// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using grpc = contracts::Dolittle.Runtime.Events;

namespace Dolittle.Events
{
    /// <summary>
    /// Defines a system that is capable of converting events to and from protobuf.
    /// </summary>
    public interface IEventConverter
    {
        /// <summary>
        /// Convert from <see cref="grpc.CommittedEvent"/> to <see cref="global::Dolittle.Events.CommittedEvent"/>.
        /// </summary>
        /// <param name="source"><see cref="grpc.CommittedEvent"/>.</param>
        /// <returns>Converted <see cref="global::Dolittle.Events.CommittedEvent"/>.</returns>
        CommittedEvent ToSDK(grpc.CommittedEvent source);
    }
}