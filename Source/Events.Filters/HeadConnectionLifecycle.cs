// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Events.Processing;
using Dolittle.Heads;
using Dolittle.Resilience;
using Dolittle.Types;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents a <see cref="ITakePartInHeadConnectionLifecycle"/> for settings up event handlers.
    /// </summary>
    public class HeadConnectionLifecycle : ITakePartInHeadConnectionLifecycle
    {
        readonly IEnumerable<IEventStreamFilter> _filters;
        readonly IStreamFilters _streamFilters;
        readonly IAsyncPolicy _startFilterProcessorPolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadConnectionLifecycle"/> class.
        /// </summary>
        /// <param name="filterProviders"><see cref="IImplementationsOf{T}"/> <see cref="ICanProvideStreamFilters"/>.</param>
        /// <param name="streamFilters">The <see cref="IStreamFilters"/> system.</param>
        /// <param name="policies"><see cref="IAsyncPolicyFor{HeadConnectionLifecycle}"/> the event handlers.</param>
        public HeadConnectionLifecycle(
            IInstancesOf<ICanProvideStreamFilters> filterProviders,
            IStreamFilters streamFilters,
            IPolicies policies)
        {
            _streamFilters = streamFilters;

            _startFilterProcessorPolicy = policies.GetAsyncNamed(typeof(StartEventProcessorPolicy).Name);
            _filters = filterProviders.SelectMany(_ => _.Provide()).ToList();
        }

        /// <inheritdoc/>
        public bool IsReady() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public async Task OnConnected(CancellationToken token)
        {
            var tasks = _filters.Select(filter =>
            {
                _streamFilters.Register(filter);
                return _startFilterProcessorPolicy.Execute((token) => _streamFilters.Start(filter, token), token);
            }).ToList();
            await Task.WhenAny(tasks).ConfigureAwait(false);
            var exception = tasks.FirstOrDefault(_ => _.Exception != null)?.Exception;
            if (exception != null) throw exception;
        }

        /// <inheritdoc/>
        public void OnDisconnected()
        {
            _filters.Select(_ => _.Identifier).ForEach(_streamFilters.DeRegister);
        }
    }
}