// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Threading.Tasks;
using Dolittle.TimeSeries.DataTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

#pragma warning disable CA1034

namespace Dolittle.TimeSeries.DataPoints.for_DataPointProcessor
{
    public class when_invoking_processor_that_takes_data_point_only
    {
        public interface Processor : ICanProcessDataPoints
        {
            Task MyProcessorMethod(DataPoint<Single> dataPoint);
        }

        static Mock<Processor> instance;
        static MethodInfo method;
        static DataPointProcessor processor;

        static DataPoint<Single> data_point;

        Establish context = () =>
        {
            instance = new Mock<Processor>();
            method = typeof(Processor).GetMethod("MyProcessorMethod", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            processor = new DataPointProcessor(instance.Object, method);
            data_point = new DataPoint<Single>
            {
                Measurement = new Single { Value = 42, Error = 43 }
            };
        };

        Because of = () => processor.Invoke(null, data_point);

        It should_invoke_the_processor_with_the_data_point = () => instance.Verify(_ => _.MyProcessorMethod(data_point), Moq.Times.Once());
    }
}