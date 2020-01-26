// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Exception that gets thrown when there are arguments for a <see cref="ICanProcessDataPoints">data point processor</see> that are unfulfilled.
    /// </summary>
    public class UnfulfilledArgumentsForDataPointProcessor : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnfulfilledArgumentsForDataPointProcessor"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanProcessDataPoints"/>.</param>
        /// <param name="method"><see cref="MethodInfo">Method</see> that represents the processor.</param>
        /// <param name="parameters">Collection of <see cref="ParameterInfo"/> that are unfulfilled.</param>
        public UnfulfilledArgumentsForDataPointProcessor(Type type, MethodInfo method, IEnumerable<ParameterInfo> parameters)
            : base($"DataPoint processor '{method.Name}' on type '{type.AssemblyQualifiedName}' has the following unfulfilled arguments '{string.Join(", ", parameters.Select(_ => _.Name))}' ")
        {
        }
    }
}