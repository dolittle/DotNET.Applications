// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.TimeSeries.DataTypes;
using Machine.Specifications;

namespace Dolittle.TimeSeries.DataPoints.for_TypeConversion
{
    public class when_converting_tag_data_point_with_single_to_runtime_and_back
    {
        static TagDataPoint tagDataPoint;
        static TagDataPoint result;

        Establish context = () =>
        {
            const string tag = "Some Tag";
            var measurement = new Single { Value = 42.43, Error = 44.45 };
            tagDataPoint = new TagDataPoint(tag, measurement);
        };

        Because of = () => result = tagDataPoint.ToRuntime().ToTagDataPoint();

        It should_hold_the_same_tag = () => result.Tag.ShouldEqual(tagDataPoint.Tag);
        It should_hold_same_value = () => ((Single)result.Measurement).Value.ShouldEqual(((Single)tagDataPoint.Measurement).Value);
        It should_hold_same_error = () => ((Single)result.Measurement).Error.ShouldEqual(((Single)tagDataPoint.Measurement).Error);
    }
}