// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Single = Dolittle.TimeSeries.DataTypes.Single;

#pragma warning disable CA1034

namespace Dolittle.TimeSeries.DataPoints.for_DataPointProcessor
{
    public class when_invoking_processor_that_takes_arguments_that_will_be_unfulfilled
    {
        public interface Processor : ICanProcessDataPoints
        {
            Task MyProcessorMethod(DataPoint<Single> dataPoint, TimeSeriesMetadata metadata, DateTimeOffset time);
        }

        static Mock<Processor> instance;
        static MethodInfo method;
        static DataPointProcessor processor;

        static DataPoint<Single> data_point;
        static TimeSeriesMetadata metadata;

        static Exception result;

        Establish context = () =>
        {
            instance = new Mock<Processor>();
            method = typeof(Processor).GetMethod("MyProcessorMethod", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            processor = new DataPointProcessor(instance.Object, method);
            data_point = new DataPoint<Single>
            {
                Measurement = new Single { Value = 42, Error = 43 }
            };
            metadata = new TimeSeriesMetadata(Guid.NewGuid());
        };

        Because of = () => result = Catch.Exception(() => processor.Invoke(metadata, data_point));

        It should_throw_unfulfilled_arguments_for_data_point_processor = () => result.ShouldBeOfExactType<UnfulfilledArgumentsForDataPointProcessor>();
    }
}