// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        readonly IFilterProcessors _filterProcessors;
        readonly IAsyncPolicyFor<HeadConnectionLifecycle> _policy;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadConnectionLifecycle"/> class.
        /// </summary>
        /// <param name="filterProviders"><see cref="IImplementationsOf{T}"/> <see cref="ICanProvideStreamFilters"/>.</param>
        /// <param name="filterProcessors">The <see cref="IFilterProcessors"/> system.</param>
        /// <param name="policy"><see cref="IAsyncPolicyFor{HeadConnectionLifecycle}"/> the event handlers.</param>
        public HeadConnectionLifecycle(
            IInstancesOf<ICanProvideStreamFilters> filterProviders,
            IFilterProcessors filterProcessors,
            IAsyncPolicyFor<HeadConnectionLifecycle> policy)
        {
            _filterProcessors = filterProcessors;
            _policy = policy;
            _filters = filterProviders.SelectMany(_ => _.Provide()).ToList();
        }

        /// <inheritdoc/>
        public bool IsReady() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public async Task OnConnected(CancellationToken token)
        {
            var tasks = _filters.Select(filter => _policy.Execute((token) => _filterProcessors.Start(filter, token), token)).ToList();
            if (tasks.Count == 0) return;
            await Task.WhenAny(tasks).ConfigureAwait(false);
            var exception = tasks.FirstOrDefault(_ => _.Exception != null)?.Exception;
            if (exception != null) throw exception;
        }
    }
}