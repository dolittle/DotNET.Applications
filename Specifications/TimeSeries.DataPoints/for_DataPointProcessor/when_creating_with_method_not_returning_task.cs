// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataPoints.for_DataPointProcessor
{
    public class when_creating_with_method_not_returning_task
    {
        internal class Processor : ICanProcessDataPoints
        {
            void MyProcessorMethod()
            {
            }
        }

        static Processor instance;
        static MethodInfo method;
        static Exception result;

        Establish context = () =>
        {
            instance = new Processor();
            method = typeof(Processor).GetMethod("MyProcessorMethod", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        };

        Because of = () => result = Catch.Exception(() => new DataPointProcessor(instance, method));

        It should_throw_data_point_processor_method_must_be_async = () => result.ShouldBeOfExactType<DataPointProcessorMethodMustBeAsync>();
    }
}