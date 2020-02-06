// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Threading.Tasks;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

#pragma warning disable CA1034

namespace Dolittle.TimeSeries.DataPoints.for_DataPointProcessor
{
    public class when_invoking_processor_that_takes_no_parameters
    {
        public interface Processor : ICanProcessDataPoints
        {
            Task MyProcessorMethod();
        }

        static Mock<Processor> instance;
        static MethodInfo method;
        static DataPointProcessor processor;

        Establish context = () =>
        {
            instance = new Mock<Processor>();
            method = typeof(Processor).GetMethod("MyProcessorMethod", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            processor = new DataPointProcessor(instance.Object, method);
        };

        Because of = () => processor.Invoke(null, null);

        It should_invoke_the_processor = () => instance.Verify(_ => _.MyProcessorMethod(), Moq.Times.Once());
    }
}