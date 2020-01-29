// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.TimeSeries.DataPoints;

namespace Dolittle.TimeSeries.Identity
{
    /// <summary>
    /// Defines a system for working with identification of <see cref="TimeSeriesId"/>.
    /// </summary>
    public interface ITimeSeriesIdentifier
    {
        /// <summary>
        /// Register a map between a given <see cref="Source"/> and <see cref="Tag"/> to a <see cref="TimeSeriesId"/>.
        /// </summary>
        /// <param name="source"><see cref="Source"/> to register for.</param>
        /// <param name="tag"><see cref="Tag"/> to register for.</param>
        /// <param name="timeSeriesId"><see cref="TimeSeriesId"/> to register for.</param>
        void Register(Source source, Tag tag, TimeSeriesId timeSeriesId);

        /// <summary>
        /// Register a map between a given <see cref="Source"/> and <see cref="Tag"/> to a <see cref="TimeSeriesId"/>.
        /// </summary>
        /// <param name="source"><see cref="Source"/> to register for.</param>
        /// <param name="map"><see cref="IDictionary{TKey,TValue}">Map</see> of <see cref="Tag"/> and <see cref="TimeSeriesId"/>.</param>
        void Register(Source source, IDictionary<Tag, TimeSeriesId> map);
    }
}