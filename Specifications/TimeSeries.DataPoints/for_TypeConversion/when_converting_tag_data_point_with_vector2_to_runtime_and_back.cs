// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.TimeSeries.DataTypes;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataPoints.for_TypeConversion
{
    public class when_converting_tag_data_point_with_vector2_to_runtime_and_back
    {
        static TagDataPoint tagDataPoint;
        static TagDataPoint result;

        Establish context = () =>
        {
            const string tag = "Some Tag";

            var measurement = new Vector2
            {
                X = new Single { Value = 42.43, Error = 44.45 },
                Y = new Single { Value = 46.47, Error = 48.49 }
            };

            tagDataPoint = new TagDataPoint(tag, measurement);
        };

        Because of = () => result = tagDataPoint.ToRuntime().ToTagDataPoint();

        It should_hold_the_same_tag = () => result.Tag.ShouldEqual(tagDataPoint.Tag);
        It should_hold_same_x_value = () => ((Vector2)result.Measurement).X.Value.ShouldEqual(((Vector2)tagDataPoint.Measurement).X.Value);
        It should_hold_same_x_error = () => ((Vector2)result.Measurement).X.Error.ShouldEqual(((Vector2)tagDataPoint.Measurement).X.Error);
        It should_hold_same_y_value = () => ((Vector2)result.Measurement).Y.Value.ShouldEqual(((Vector2)tagDataPoint.Measurement).Y.Value);
        It should_hold_same_y_error = () => ((Vector2)result.Measurement).Y.Error.ShouldEqual(((Vector2)tagDataPoint.Measurement).Y.Error);
    }
}