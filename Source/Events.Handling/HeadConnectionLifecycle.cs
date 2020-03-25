// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        readonly IImplementationsOf<ICanHandleEvents> _eventHandlerTypes;
        readonly IEventHandlers _eventHandlers;
        readonly IEventProcessingCompletion _eventProcessingCompletion;
        readonly IAsyncPolicyFor<HeadConnectionLifecycle> _policy;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadConnectionLifecycle"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes"><see cref="IImplementationsOf{T}"/> <see cref="ICanHandleEvents"/>.</param>
        /// <param name="eventHandlers">The <see cref="IEventHandlers"/> system.</param>
        /// <param name="eventProcessingCompletion"><see cref="IEventProcessingCompletion"/> for registering event handlers.</param>
        /// <param name="policy"><see cref="IAsyncPolicyFor{HeadConnectionLifecycle}"/> the event handlers.</param>
        public HeadConnectionLifecycle(
            IImplementationsOf<ICanHandleEvents> eventHandlerTypes,
            IEventHandlers eventHandlers,
            IEventProcessingCompletion eventProcessingCompletion,
            IAsyncPolicyFor<HeadConnectionLifecycle> policy)
        {
            _eventHandlerTypes = eventHandlerTypes;
            _eventHandlers = eventHandlers;
            _eventProcessingCompletion = eventProcessingCompletion;
            _policy = policy;
        }

        /// <inheritdoc/>
        public bool IsReady() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public async Task OnConnected(CancellationToken token)
        {
            var tasks = _eventHandlerTypes.Select(type =>
            {
                var eventHandler = _eventHandlers.Register(type);
                _eventProcessingCompletion.RegisterHandler(eventHandler);
                return _policy.Execute((token) => _eventHandlers.Start(eventHandler, token), token);
            }).ToList();
            await Task.WhenAny(tasks).ConfigureAwait(false);
            var exception = tasks.FirstOrDefault(_ => _.Exception != null)?.Exception;
            if (exception != null) throw exception;
        }
    }
}