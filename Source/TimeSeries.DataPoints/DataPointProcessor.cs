// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents a <see cref="DataPointProcessor"/>.
    /// </summary>
    public class DataPointProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointProcessor"/> class.
        /// </summary>
        /// <param name="processor">The actual <see cref="ICanProcessDataPoints">processor</see>.</param>
        /// <param name="method">The <see cref="MethodInfo">method</see> that processes.</param>
        public DataPointProcessor(ICanProcessDataPoints processor, MethodInfo method)
        {
            Id = Guid.NewGuid();
            Processor = processor;
            Method = method;

            ThrowIfMethodDoesNotRunAsynchronous(processor, method);
        }

        /// <summary>
        /// Gets the <see cref="DataPointProcessorId">unique identifier</see> for the <see cref="DataPointProcessor"/>.
        /// </summary>
        public DataPointProcessorId Id { get; }

        /// <summary>
        /// Gets the <see cref="ICanProcessDataPoints">processor</see> instance.
        /// </summary>
        public ICanProcessDataPoints Processor { get; }

        /// <summary>
        /// Gets the <see cref="MethodInfo">method</see> that represents the processor.
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Invoke the processor.
        /// </summary>
        /// <param name="metadata">The <see cref="TimeSeriesMetadata"/>.</param>
        /// <param name="dataPoint"><see cref="DataPoint{T}"/> to process.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task Invoke(TimeSeriesMetadata metadata, object dataPoint)
        {
            var parameters = Method.GetParameters();
            var arguments = new object[parameters.Length];

            for (var parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                var parameter = parameters[parameterIndex];

                if (parameter.ParameterType == typeof(TimeSeriesMetadata)) arguments[parameterIndex] = metadata;
                if (parameter.ParameterType.IsGenericType &&
                    parameter.ParameterType.GetGenericTypeDefinition() == typeof(DataPoint<>))
                {
                    arguments[parameterIndex] = dataPoint;
                }
            }

            var missingArguments = new List<ParameterInfo>();

            for (var parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                var parameter = parameters[parameterIndex];
                if (arguments[parameterIndex] == null) missingArguments.Add(parameter);
            }

            if (missingArguments.Count > 0) throw new UnfulfilledArgumentsForDataPointProcessor(Processor.GetType(), Method, missingArguments);

            return Method.Invoke(Processor, arguments) as Task;
        }

        void ThrowIfMethodDoesNotRunAsynchronous(ICanProcessDataPoints processor, MethodInfo method)
        {
            if (method.ReturnType != typeof(Task))
                throw new DataPointProcessorMethodMustBeAsync(processor.GetType(), method);
        }
    }
}