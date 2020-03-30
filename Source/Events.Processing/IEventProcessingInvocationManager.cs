// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Google.Protobuf;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that manages the invocations.
    /// </summary>
    public interface IEventProcessingInvocationManager
    {
        /// <summary>
        /// Gets the <see cref="IEventProcessingInvocationManagerFor{TProcessingResponse, TProcessingResult}" /> for an event processor.
        /// </summary>
        /// <typeparam name="TProcessingResponse">The processing response <see cref="IMessage" />.</typeparam>
        /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" />.</typeparam>
        /// <returns><see cref="IEventProcessingInvocationManagerFor{TProcessingResponse, TProcessingResult}" />.</returns>
        IEventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult> GetInvocationManagerFor<TProcessingResponse, TProcessingResult>()
            where TProcessingResponse : IMessage, new()
            where TProcessingResult : IProcessingResult;
    }
}