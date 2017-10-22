/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using doLittle.Execution;
using doLittle.DependencyInversion;
using doLittle.Time;
using doLittle.Runtime.Events;
using doLittle.Runtime.Applications;
using doLittle.Runtime.Events.Processing;
using doLittle.Logging;

namespace doLittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessor"/> for systems marked with the
    /// <see cref="ICanProcessEvents"/> marker interface and has the "Process" method according to the
    /// convention
    /// </summary>
    public class ProcessMethodEventProcessor : IEventProcessor
    {
        readonly IContainer _container;
        readonly MethodInfo _methodInfo;
        readonly ISystemClock _systemClock;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ProcessMethodEventProcessor"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to use for getting instances of <see cref="ICanProcessEvents"/> implementation</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for timing purposes</param>
        /// <param name="identifier"><see cref="EventProcessorIdentifier"/> that uniquely identifies the <see cref="ProcessMethodEventProcessor"/></param>
        /// <param name="event"><see cref="IApplicationResourceIdentifier">Identifier</see> for identifying the <see cref="IEvent"/></param>
        /// <param name="methodInfo"><see cref="MethodInfo"/> for the actual process method</param>
        /// <param name="logger"></param>
        public ProcessMethodEventProcessor(
            IContainer container,
            ISystemClock systemClock,
            EventProcessorIdentifier identifier,
            IApplicationResourceIdentifier @event,
            MethodInfo methodInfo,
            ILogger logger)
        {
            Identifier = identifier;
            Event = @event;

            _container = container;
            _systemClock = systemClock;
            _methodInfo = methodInfo;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IApplicationResourceIdentifier Event { get; }

        /// <inheritdoc/>
        public EventProcessorIdentifier Identifier { get; }

        /// <inheritdoc/>
        public IEventProcessingResult Process(IEventEnvelope envelope, IEvent @event)
        {
            _logger.Trace($"Process through a process method");
            var status = EventProcessingStatus.Success;
            var messages = new EventProcessingMessage[0];

            _logger.Trace("Get current time");
            var start = _systemClock.GetCurrentTime();

            _logger.Trace($"Start : {start}");

            try
            {
                _logger.Trace($"Process event using {_methodInfo.DeclaringType}");
                var processor = _container.Get(_methodInfo.DeclaringType);
                _logger.Trace("Invoke method");
                _methodInfo.Invoke(processor, new[] { @event });
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Failed processing");
                status = EventProcessingStatus.Failed;
                messages = new[] {
                    new EventProcessingMessage(EventProcessingMessageSeverity.Error, exception.Message, exception.StackTrace.Split(Environment.NewLine.ToCharArray()))
                };
            }
            var end = _systemClock.GetCurrentTime();

            return new EventProcessingResult(envelope.CorrelationId, this, status, start, end, messages);
        }
    }
}
