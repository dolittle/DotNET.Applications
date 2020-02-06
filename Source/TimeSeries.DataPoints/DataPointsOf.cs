// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the filtering of <see cref="DataPoint{T}">data points</see>.
    /// </summary>
    /// <typeparam name="TValue">Type of <see cref="IMeasurement">measurement</see> for the <see cref="DataPoint{T}"/>.</typeparam>
    public class DataPointsOf<TValue>
        where TValue : IMeasurement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointsOf{TValue}"/> class.
        /// </summary>
        /// <param name="specification"><see cref="Specification{T}"/> for the filtering.</param>
        public DataPointsOf(Specification<DataPoint<TValue>> specification)
        {
            ValueSpecification = specification;
        }

        /// <summary>
        /// Gets or sets the specification for filtering <see cref="TimeSeriesMetadata"/>.
        /// </summary>
        public Specification<TimeSeriesMetadata> MetadataSpecification { get; set; }

        /// <summary>
        /// Gets or sets the specification for filtering values within the <see cref="DataPoint{T}"/>.
        /// </summary>
        public Specification<DataPoint<TValue>> ValueSpecification { get; set; }
    }
}