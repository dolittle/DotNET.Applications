// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Extends <see cref="DataPointsOf{T}"/> with rules for <see cref="Origin"/>.
    /// </summary>
    public static class OriginExtensions
    {
        /// <summary>
        /// Configure what origin for a <see cref="DataPointsOf{T}"/>.
        /// </summary>
        /// <typeparam name="TValue">Type of <see cref="IMeasurement">measurement</see> for the <see cref="DataPoint{T}"/>.</typeparam>
        /// <param name="filter"><see cref="DataPointsOf{T}"/> to configure.</param>
        /// <param name="origin"><see cref="Origin"/>.</param>
        /// <returns>Continued <see cref="DataPointsOf{T}"/>.</returns>
        public static DataPointsOf<TValue> OriginatingFrom<TValue>(this DataPointsOf<TValue> filter, Origin origin)
            where TValue : IMeasurement
        {
            filter.ValueSpecification = filter.ValueSpecification.And(new OriginOf<TValue>(origin));
            return filter;
        }
    }
}