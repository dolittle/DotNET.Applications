// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the starting point for creating filters for <see cref="DataPoint{T}">data points</see>.
    /// </summary>
    public static class DataPoints
    {
        /// <summary>
        /// Filter of a specific type of <see cref="DataPoint{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of data point.</typeparam>
        /// <returns><see cref="DataPointsOf{T}"/> filter.</returns>
        public static DataPointsOf<T> Of<T>()
            where T : IMeasurement
        {
            return new DataPointsOf<T>(new DataPointsOfSpecification<T>());
        }
    }
}