/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

/// <summary>
/// A method is marked as an EventProcessor but does not have the correct method signature
/// 
/// void [MethodName](MyEvent @event)
/// void [MethodName](MyEvent @event, EventSourceId id)
/// void [MethodName](MyEvent @event, EventMetadata eventMetadata)
/// </summary>
[Serializable]
public class EventProcessorMethodParameterMismatch : Exception
{
    private const string STANDARD_ERROR_MSG = @"
        An event processor method must conform to one of the following method signtaures:
        void [MethodName](MyEvent @event)
        void [MethodName](MyEvent @event, EventSourceId id)
        void [MethodName](MyEvent @event, EventMetadata eventMetadata)
    ";

    /// <summary>
    ///     Initializes a new instance of the EventProcessorMethodParameterMismatch custom exception
    /// </summary>
    public EventProcessorMethodParameterMismatch() : base(STANDARD_ERROR_MSG)
    {}

    /// <summary>
    ///     Initializes a new instance of the EventProcessorMethodParameterMismatch custom exception
    /// </summary>
    /// <param name="message">A message describing the exception</param>
    public EventProcessorMethodParameterMismatch(string message)
        : base(message)
    {}

    /// <summary>
    ///     Initializes a new instance of the EventProcessorMethodParameterMismatch custom exception
    /// </summary>
    /// <param name="message">A message describing the exception</param>
    /// <param name="innerException">An inner exception that is the original source of the error</param>
    public EventProcessorMethodParameterMismatch(string message, Exception innerException)
        : base(message, innerException)
    {}

    /// <summary>
    ///     Initializes a new instance of the EventProcessorMethodParameterMismatch custom exception
    /// </summary>
    /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
    /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
    protected EventProcessorMethodParameterMismatch(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {}
}