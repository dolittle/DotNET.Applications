// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Heads;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.Identity.Runtime;
using static Dolittle.TimeSeries.Identity.Runtime.TimeSeriesMapIdentifier;

namespace Dolittle.TimeSeries.Identity
{
    /// <summary>
    /// Represents an implementation of <see cref="ITimeSeriesIdentifier"/>.
    /// </summary>
    public class TimeSeriesIdentifier : ITimeSeriesIdentifier
    {
        readonly IClientFor<TimeSeriesMapIdentifierClient> _timeSeriesMapIdentifierClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSeriesIdentifier"/> class.
        /// </summary>
        /// <param name="timeSeriesMapIdentifierClient"><see cref="IClientFor{T}"/> <see cref="TimeSeriesMapIdentifierClient"/>.</param>
        public TimeSeriesIdentifier(IClientFor<TimeSeriesMapIdentifierClient> timeSeriesMapIdentifierClient)
        {
            _timeSeriesMapIdentifierClient = timeSeriesMapIdentifierClient;
        }

        /// <inheritdoc/>
        public void Register(Source source, Tag tag, TimeSeriesId timeSeriesId)
        {
            var timeSeriesMap = new TimeSeriesMap
            {
                Source = source,
            };
            timeSeriesMap.TagToTimeSeriesId[tag] = timeSeriesId.ToProtobuf();
            _timeSeriesMapIdentifierClient.Instance.RegisterAsync(timeSeriesMap);
        }

        /// <inheritdoc/>
        public void Register(Source source, IDictionary<Tag, TimeSeriesId> map)
        {
            var timeSeriesMap = new TimeSeriesMap
            {
                Source = source,
            };
            foreach ((var tag, var timeSeriesId) in map)
            {
                timeSeriesMap.TagToTimeSeriesId[tag] = timeSeriesId.ToProtobuf();
            }

            _timeSeriesMapIdentifierClient.Instance.RegisterAsync(timeSeriesMap);
        }
    }
}