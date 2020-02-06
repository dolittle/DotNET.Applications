// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents a filter for a specific origin.
    /// </summary>
    /// <typeparam name="TValue">Type of <see cref="IMeasurement">measurement</see> for the <see cref="DataPoint{T}"/>.</typeparam>
    public class OriginOf<TValue> : Specification<DataPoint<TValue>>
        where TValue : IMeasurement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OriginOf{TValue}"/> class.
        /// </summary>
        /// <param name="origin"><see cref="Origin"/> to filter on.</param>
        public OriginOf(Origin origin)
        {
            // Predicate = dataPoint => dataPoint.Origin == origin;
            Origin = origin;
        }

        /// <summary>
        /// Gets the origin for the filter.
        /// </summary>
        public Origin Origin { get; }
    }
}