// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Events.Processing;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Grpc.Core;
using static contracts::Dolittle.Runtime.Events.Processing.PublicFilters;

namespace Dolittle.Events.Filters.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="IFilterProcessor" /> that can process <see cref="ICanFilterPublicEvents" /> filters.
    /// </summary>
    public class PublicEventsFilterProcessor : IFilterProcessor
    {
        readonly IEventProcessors _eventProcessors;
        readonly PublicFiltersClient _filtersClient;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicEventsFilterProcessor"/> class.
        /// </summary>
        /// <param name="eventProcessors">The <see cref="IEventProcessors" />.</param>
        /// <param name="filtersClient"><see cref="PublicFiltersClient"/> for connecting to server.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public PublicEventsFilterProcessor(
            IEventProcessors eventProcessors,
            PublicFiltersClient filtersClient,
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
            _logger.Information($"Starting processor for external filter with identifier '{filter.Identifier}' on source stream '{filter.SourceStreamId}'");

            var additionalInfo = new PublicFilterArguments
            {
                Filter = filter.Identifier.ToProtobuf()
            };
            var metadata = new Metadata { additionalInfo.ToArgumentsMetadata() };

            var result = _filtersClient.Connect(metadata);

            _eventProcessors.RegisterAndStartProcessing<PublicFilterClientToRuntimeResponse, PublicFilterRuntimeToClientRequest, IFilterResult>(
                ScopeId.Default,
                StreamId.AllStream,
                filter.Identifier,
                result,
                _ => _.CallNumber,
                _ => _.CallNumber,
                request => new PublicEventsProcessingRequestProxy(request),
                (result, request) => new PublicEventsProcessingResponseProxy(result, request),
                (failureReason, retry, retryTimeout) => retry ? new RetryFilteringResult(retryTimeout, failureReason) as IFilterResult : new FailedFilteringResult(failureReason) as IFilterResult,
                async @event =>
                {
                    var filterResult = await filter.Filter(@event).ConfigureAwait(false);
                    return new SucceededFilteringResult(filterResult.IsIncluded, filterResult.Partition);
                });
        }
    }
}