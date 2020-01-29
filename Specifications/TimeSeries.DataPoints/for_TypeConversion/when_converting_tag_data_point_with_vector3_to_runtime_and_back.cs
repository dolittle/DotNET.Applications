// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.TimeSeries.DataTypes;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataPoints.for_TypeConversion
{
    public class when_converting_tag_data_point_with_vector3_to_runtime_and_back
    {
        static TagDataPoint tagDataPoint;
        static TagDataPoint result;

        Establish context = () =>
        {
            const string tag = "Some Tag";

            var measurement = new Vector3
            {
                X = new Single { Value = 42.43, Error = 44.45 },
                Y = new Single { Value = 46.47, Error = 48.49 },
                Z = new Single { Value = 50.51, Error = 52.53 }
            };

            tagDataPoint = new TagDataPoint(tag, measurement);
        };

        Because of = () => result = tagDataPoint.ToRuntime().ToTagDataPoint();

        It should_hold_the_same_tag = () => result.Tag.ShouldEqual(tagDataPoint.Tag);
        It should_hold_same_x_value = () => ((Vector3)result.Measurement).X.Value.ShouldEqual(((Vector3)tagDataPoint.Measurement).X.Value);
        It should_hold_same_x_error = () => ((Vector3)result.Measurement).X.Error.ShouldEqual(((Vector3)tagDataPoint.Measurement).X.Error);
        It should_hold_same_y_value = () => ((Vector3)result.Measurement).Y.Value.ShouldEqual(((Vector3)tagDataPoint.Measurement).Y.Value);
        It should_hold_same_y_error = () => ((Vector3)result.Measurement).Y.Error.ShouldEqual(((Vector3)tagDataPoint.Measurement).Y.Error);
        It should_hold_same_z_value = () => ((Vector3)result.Measurement).Z.Value.ShouldEqual(((Vector3)tagDataPoint.Measurement).Z.Value);
        It should_hold_same_z_error = () => ((Vector3)result.Measurement).Z.Error.ShouldEqual(((Vector3)tagDataPoint.Measurement).Z.Error);
    }
}