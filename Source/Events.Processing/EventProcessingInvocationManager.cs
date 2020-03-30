// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Lifecycle;
using Google.Protobuf;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingInvocationManager" />.
    /// </summary>
    [Singleton]
    public class EventProcessingInvocationManager : IEventProcessingInvocationManager
    {
        /// <inheritdoc/>
        public IEventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult> GetInvocationManagerFor<TProcessingResponse, TProcessingResult>()
            where TProcessingResponse : IMessage, new()
            where TProcessingResult : IProcessingResult => new EventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult>();
    }
}