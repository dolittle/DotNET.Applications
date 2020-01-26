// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Extension methods for conversion of types.
    /// </summary>
    public static class TypeConversion
    {
        /// <summary>
        /// Convert from <see cref="TagDataPoint"/> to <see cref="Runtime.TagDataPoint"/>.
        /// </summary>
        /// <param name="tagDataPoint"><see cref="TagDataPoint"/> to convert from.</param>
        /// <returns>Converted <see cref="Runtime.TagDataPoint"/>.</returns>
        public static Runtime.TagDataPoint ToRuntime(this TagDataPoint tagDataPoint)
        {
            var converted = new Runtime.TagDataPoint
            {
                Tag = tagDataPoint.Tag,
            };

            switch (tagDataPoint.Measurement)
            {
                case Single single:
                    converted.SingleValue = single.ToRuntime();
                    break;
                case Vector2 vector2:
                    converted.Vector2Value = vector2.ToRuntime();
                    break;
                case Vector3 vector3:
                    converted.Vector3Value = vector3.ToRuntime();
                    break;
            }

            return converted;
        }

        /// <summary>
        /// Convert from <see cref="Runtime.TagDataPoint"/> to <see cref="TagDataPoint"/>.
        /// </summary>
        /// <param name="tagDataPoint"><see cref="Runtime.TagDataPoint"/> to convert from.</param>
        /// <returns>Converted <see cref="TagDataPoint"/>.</returns>
        public static TagDataPoint ToTagDataPoint(this Runtime.TagDataPoint tagDataPoint)
        {
            var tag = tagDataPoint.Tag;
            IMeasurement measurement = null;

            switch (tagDataPoint.MeasurementCase)
            {
                case Runtime.TagDataPoint.MeasurementOneofCase.SingleValue:
                    measurement = tagDataPoint.SingleValue.ToSingle();
                    break;
                case Runtime.TagDataPoint.MeasurementOneofCase.Vector2Value:
                    measurement = tagDataPoint.Vector2Value.ToVector2();
                    break;
                case Runtime.TagDataPoint.MeasurementOneofCase.Vector3Value:
                    measurement = tagDataPoint.Vector3Value.ToVector3();
                    break;
            }

            return new TagDataPoint(tag, measurement);
        }
    }
}