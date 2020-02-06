// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_TypeConversion
{
    public class when_converting_vector2_to_runtime_and_back
    {
        static Vector2 measurement;
        static Vector2 result;

        Establish context = () => measurement = new Vector2
        {
            X = new Single { Value = 42.43, Error = 44.45 },
            Y = new Single { Value = 46.47, Error = 48.49 }
        };

        Because of = () => result = measurement.ToRuntime().ToVector2();

        It should_hold_same_x_value = () => result.X.Value.ShouldEqual(measurement.X.Value);
        It should_hold_same_x_error = () => result.X.Error.ShouldEqual(measurement.X.Error);
        It should_hold_same_y_value = () => result.Y.Value.ShouldEqual(measurement.Y.Value);
        It should_hold_same_y_error = () => result.Y.Error.ShouldEqual(measurement.Y.Error);
    }
}