// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Represents a datapoint in a <see cref="TimeSeries"/>.
    /// </summary>
    /// <typeparam name="TValue">Type of <see cref="IMeasurement">measurement</see> for the <see cref="DataPoint{T}"/>.</typeparam>
    public class DataPoint<TValue>
        where TValue : IMeasurement
    {
        /// <summary>
        /// Gets or sets the <see cref="TimeSeries"/> the <see cref="DataPoint{T}"/> belongs to.
        /// </summary>
        public TimeSeriesId TimeSeries { get; set; }

        /// <summary>
        /// Gets or sets the value for the <see cref="DataPoint{T}"/>.
        /// </summary>
        public TValue Measurement { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Timestamp"/>.
        /// </summary>
        public Timestamp Timestamp { get; set; }
    }
}