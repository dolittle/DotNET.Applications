// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.TimeSeries.DataTypes.for_TypeConversion
{
    public class when_converting_vector3_to_runtime_and_back
    {
        static Vector3 measurement;
        static Vector3 result;

        Establish context = () => measurement = new Vector3
        {
            X = new Single { Value = 42.43, Error = 44.45 },
            Y = new Single { Value = 46.47, Error = 48.49 },
            Z = new Single { Value = 50.51, Error = 52.53 }
        };

        Because of = () => result = measurement.ToRuntime().ToVector3();

        It should_hold_same_x_value = () => result.X.Value.ShouldEqual(measurement.X.Value);
        It should_hold_same_x_error = () => result.X.Error.ShouldEqual(measurement.X.Error);
        It should_hold_same_y_value = () => result.Y.Value.ShouldEqual(measurement.Y.Value);
        It should_hold_same_y_error = () => result.Y.Error.ShouldEqual(measurement.Y.Error);
        It should_hold_same_z_value = () => result.Z.Value.ShouldEqual(measurement.Z.Value);
        It should_hold_same_z_error = () => result.Z.Error.ShouldEqual(measurement.Z.Error);
    }
}