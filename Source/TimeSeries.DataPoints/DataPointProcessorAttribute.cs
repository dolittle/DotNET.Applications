// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the attribute that decorates methods as processors of <see cref="DataPoint{T}"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DataPointProcessorAttribute : Attribute
    {
    }
}