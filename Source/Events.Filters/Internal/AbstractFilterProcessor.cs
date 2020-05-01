// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Protobuf.Contracts;
using Dolittle.Runtime.Events.Processing.Contracts;
using Google.Protobuf.WellKnownTypes;
using Contracts = Dolittle.Runtime.Events.Contracts;
using Failure = Dolittle.Protobuf.Failure;

namespace Dolittle.Events.Filters.Internal
{
    /// <summary>
    /// Represents an event filter processor that wraps the gRPC protocol for event filters.
    /// </summary>
    public abstract class AbstractFilterProcessor
    {
        readonly IEventConverter _converter;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractFilterProcessor"/> class.
        /// </summary>
        /// <param name="converter">The <see cref="IEventConverter"/> to use for converting events.</param>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        protected AbstractFilterProcessor(IEventConverter converter, ILogger logger)
        {
            _converter = converter;
            _logger = logger;
        }

        /// <summary>
        /// Gets the potential <see cref="Failure"/> returned from the registration request.
        /// </summary>
        public abstract Failure RegisterFailure { get; }

        /// <summary>
        /// Registers the event filter with the Runtime.
        /// </summary>
        /// <param name="filter">The unique <see cref="FilterId"/> for the filter.</param>
        /// <param name="scope">The <see cref="ScopeId"/> of the scope in the Event Store where the filter will run.</param>
        /// <param name="cancellationToken">Token that can be used to cancel this operation.</param>
        /// <returns>A <see cref="Task" /> that, when resolved, returns whether a registration response was received.</returns>
        public abstract Task<bool> Register(FilterId filter, ScopeId scope, CancellationToken cancellationToken);

        /// <summary>
        /// Handles event filter request from the Runtime.
        /// </summary>
        /// <param name="cancellationToken">Token that can be used to cancel this operation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public abstract Task Handle(CancellationToken cancellationToken);

        /// <summary>
        /// Invokes the provided filter with the comitted event.
        /// </summary>
        /// <typeparam name="TEventType">The event type that the filter can handle.</typeparam>
        /// <typeparam name="TFilterResult">The type of filter result that the filter returns.</typeparam>
        /// <param name="filter">The <see cref="ICanFilter{TEventType, TFilterResult}"/> to invoke.</param>
        /// <param name="event">The <see cref="Contracts.CommittedEvent"/> to invoke the filter with.</param>
        /// <param name="retry">The <see cref="RetryProcessingState"/> to use for the filter invocation.</param>
        /// <param name="scope">The <see cref="ScopeId"/> that the filter was registered on.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        protected Task<TFilterResult> InvokeFilter<TEventType, TFilterResult>(ICanFilter<TEventType, TFilterResult> filter, Contracts.CommittedEvent @event, RetryProcessingState retry, Uuid scope)
            where TEventType : IEvent
        {
            var converted = _converter.ToSDK(@event);
            if (converted.Event is TEventType typedEvent)
            {
                _logger.Trace("Invoking filter {Filter} with {Event} in {Scope}. {Retry}.", filter.GetType(), converted.Event, scope.To<ScopeId>(), retry);
                return filter.Filter(typedEvent, new EventContext(converted.EventSource, converted.Occurred));
            }

            throw new EventTypeIsIncorrectForFilter(typeof(TEventType), converted.Event.GetType());
        }

        /// <summary>
        /// Creates a <see cref="ProcessorFailure"/> from an <see cref="Exception"/> that occured during filter invocation.
        /// </summary>
        /// <typeparam name="TEventType">The event type that the filter can handle.</typeparam>
        /// <typeparam name="TFilterResult">The type of filter result that the filter returns.</typeparam>
        /// <param name="filter">The <see cref="ICanFilter{TEventType, TFilterResult}"/> that was invoked.</param>
        /// <param name="ex">The <see cref="Exception"/> that occured during filter invocation.</param>
        /// <param name="retry">The <see cref="RetryProcessingState"/> used for the filter invocation.</param>
        /// <returns>A <see cref="ProcessorFailure"/> with failure reason and retry strategy defined.</returns>
        protected ProcessorFailure CreateFailureFrom<TEventType, TFilterResult>(ICanFilter<TEventType, TFilterResult> filter, Exception ex, RetryProcessingState retry)
            where TEventType : IEvent
        {
            while (ex.InnerException != null) ex = ex.InnerException;

            _logger.Warning(ex, "Error while invoking {Filter}. Will retry later.", filter.GetType());

            return new ProcessorFailure
            {
                Reason = ex.Message,
                Retry = true,
                RetryTimeout = Duration.FromTimeSpan(TimeSpan.FromSeconds(Math.Min(retry.RetryCount * 5, 60))),
            };
        }
    }
}