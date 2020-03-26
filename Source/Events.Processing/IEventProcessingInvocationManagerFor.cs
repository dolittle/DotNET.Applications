// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Protobuf;
using grpc = contracts::Dolittle.Runtime.Events.Processing;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that can manage the invocation of the processing of an event.
    /// </summary>
    /// <typeparam name="TProcessingResponse">The processing response <see cref="IMessage" /> type.</typeparam>
    /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" /> type.</typeparam>
    public interface IEventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult>
        where TProcessingResponse : IMessage, new()
        where TProcessingResult : IProcessingResult
    {
        /// <summary>
        /// Manages the invocation of the processing of an event in a stream.
        /// </summary>
        /// <param name="invoke">The invocation <see cref="Func{T1}" /> callback.</param>
        /// <param name="eventProcessorType">The <see cref="Type" /> of the event processor.</param>
        /// <param name="partition">The <see cref="PartitionId" />.</param>
        /// <param name="retryProcessingState">The <see cref="grpc.RetryProcessingState" /> of the request.</param>
        /// <param name="createResponseFromSucceededProcessingResult">The <see cref="Func{T1, T2}" /> for creating a processing response from processing result.</param>
        /// <param name="processorFailurePropertyExpression">An <see cref="Expression{T}"/> for describing what property on response message that will hold the <see cref="grpc.ProcessorFailure" />..</param>
        /// <returns>A task that yields <see cref="IProcessingResult" />.</returns>
        Task<TProcessingResponse> Invoke(
            Func<Task<TProcessingResult>> invoke,
            Type eventProcessorType,
            PartitionId partition,
            grpc.RetryProcessingState retryProcessingState,
            Func<TProcessingResult, TProcessingResponse> createResponseFromSucceededProcessingResult,
            Expression<Func<TProcessingResponse, grpc.ProcessorFailure>> processorFailurePropertyExpression);
    }
}