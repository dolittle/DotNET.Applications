// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Reflection;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Events.Processing;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Reflection;
using Grpc.Core;
using static contracts::Dolittle.Runtime.Events.Processing.Filters;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents an implementation of <see cref="IFilterProcessor" /> that can process <see cref="ICanFilterPrivateEvents" /> filters.
    /// </summary>
    public class FilterProcessor : IFilterProcessor
    {
        readonly IEventProcessors _eventProcessors;
        readonly FiltersClient _filtersClient;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterProcessor"/> class.
        /// </summary>
        /// <param name="eventProcessors">The <see cref="IEventProcessors" />.</param>
        /// <param name="filtersClient"><see cref="FiltersClient"/> for connecting to server.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public FilterProcessor(
            IEventProcessors eventProcessors,
            FiltersClient filtersClient,
            ILogger logger)
        {
            _eventProcessors = eventProcessors;
            _filtersClient = filtersClient;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanProcess(IEventStreamFilter filter) => typeof(ICanFilterPrivateEvents).IsAssignableFrom(filter.GetType());

        /// <inheritdoc/>
        public void Start(IEventStreamFilter filter)
        {
            if (!CanProcess(filter)) throw new FilterProcessorCannotStartProcessingFilter(this, filter);
            _logger.Information($"Starting processor for private filter with identifier '{filter.Identifier}' on source stream '{filter.SourceStreamId}'");

            var scope = filter.GetType().HasAttribute<ScopeAttribute>() ? filter.GetType().GetCustomAttribute<ScopeAttribute>().Id : ScopeId.Default;
            var additionalInfo = new FilterArguments
            {
                Filter = filter.Identifier.ToProtobuf(),
                Scope = scope.ToProtobuf()
            };
            var metadata = new Metadata { additionalInfo.ToArgumentsMetadata() };

            var result = _filtersClient.Connect(metadata);

            _eventProcessors.RegisterAndStartProcessing<FilterClientToRuntimeResponse, FilterRuntimeToClientRequest, IFilterResult>(
                ScopeId.Default,
                StreamId.AllStream,
                filter.Identifier,
                result,
                _ => _.CallNumber,
                _ => _.CallNumber,
                request => new FilterProcessingRequestProxy(request),
                (result, request) => new FilterProcessingResponseProxy(result, request),
                (failureReason, retry, retryTimeout) => retry ? new RetryFilteringResult(retryTimeout, failureReason) as IFilterResult : new FailedFilteringResult(failureReason) as IFilterResult,
                async @event =>
                {
                    var filterResult = await filter.Filter(@event).ConfigureAwait(false);
                    return new SucceededFilteringResult(filterResult.IsIncluded, filterResult.Partition);
                });
        }
    }
}