// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_TypeConversion
{
    public class when_converting_single_to_runtime_and_back
    {
        static Single measurement;
        static Single result;

        Establish context = () => measurement = new Single { Value = 42.43, Error = 44.45 };

        Because of = () => result = measurement.ToRuntime().ToSingle();

        It should_hold_same_value = () => result.Value.ShouldEqual(measurement.Value);
        It should_hold_same_error = () => result.Error.ShouldEqual(measurement.Error);
    }
}