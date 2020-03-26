// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Events.Processing;
using Dolittle.Heads;
using Dolittle.Resilience;
using Dolittle.Types;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents a <see cref="ITakePartInHeadConnectionLifecycle"/> for settings up event handlers.
    /// </summary>
    public class HeadConnectionLifecycle : ITakePartInHeadConnectionLifecycle
    {
        readonly IEnumerable<AbstractEventHandler> _abstractEventHandlers;
        readonly IEventHandlerProcessor _eventHandlerProcessor;
        readonly IEventProcessingCompletion _eventProcessingCompletion;
        readonly IAsyncPolicy _startEventHandlerPolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadConnectionLifecycle"/> class.
        /// </summary>
        /// <param name="eventHandlerProviders"><see cref="IImplementationsOf{T}"/> <see cref="ICanProvideEventHandlers"/>.</param>
        /// <param name="eventHandlerProcessor">The <see cref="IEventHandlerProcessor"/> system.</param>
        /// <param name="eventProcessingCompletion"><see cref="IEventProcessingCompletion"/> for registering event handlers.</param>
        /// <param name="policies"><see cref="IAsyncPolicyFor{HeadConnectionLifecycle}"/> the event handlers.</param>
        public HeadConnectionLifecycle(
            IInstancesOf<ICanProvideEventHandlers> eventHandlerProviders,
            IEventHandlerProcessor eventHandlerProcessor,
            IEventProcessingCompletion eventProcessingCompletion,
            IPolicies policies)
        {
            _eventHandlerProcessor = eventHandlerProcessor;
            _eventProcessingCompletion = eventProcessingCompletion;

            _startEventHandlerPolicy = policies.GetAsyncNamed(typeof(StartEventProcessorPolicy).Name);
            _abstractEventHandlers = eventHandlerProviders.SelectMany(_ => _.Provide()).ToList();
        }

        /// <inheritdoc/>
        public bool IsReady() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public async Task OnConnected(CancellationToken token)
        {
            var tasks = _abstractEventHandlers.Select(eventHandler =>
            {
                _eventProcessingCompletion.RegisterHandler(eventHandler);
                return _startEventHandlerPolicy.Execute((token) => _eventHandlerProcessor.Start(eventHandler, token), token);
            }).ToList();
            await Task.WhenAny(tasks).ConfigureAwait(false);
            var exception = tasks.FirstOrDefault(_ => _.Exception != null)?.Exception;
            if (exception != null) throw exception;
        }
    }
}