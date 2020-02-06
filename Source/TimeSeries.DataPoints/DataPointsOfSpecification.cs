// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Specifications;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the base <see cref="Specification{T}"/> for a <see cref="DataPoint{T}"/> of a
    /// specific type.
    /// </summary>
    /// <typeparam name="TValue">Type of <see cref="IMeasurement">measurement</see> for the <see cref="DataPoint{T}"/>.</typeparam>
    public class DataPointsOfSpecification<TValue> : Specification<DataPoint<TValue>>
        where TValue : IMeasurement
    {
    }
}