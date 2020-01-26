// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Dolittle.Protobuf;

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Extension methods for conversion of types.
    /// </summary>
    public static class TypeConversion
    {
        /// <summary>
        /// Convert from <see cref="Single"/> to <see cref="Runtime.Single"/>.
        /// </summary>
        /// <param name="measurement"><see cref="Single"/> to convert from.</param>
        /// <returns>Converted <see cref="Runtime.Single"/>.</returns>
        public static Runtime.Single ToRuntime(this Single measurement)
        {
            return new Runtime.Single
            {
                Value = measurement.Value,
                Error = measurement.Error
            };
        }

        /// <summary>
        /// Convert from <see cref="Runtime.Single"/> to <see cref="Single"/>.
        /// </summary>
        /// <param name="measurement"><see cref="Runtime.Single"/> to convert from.</param>
        /// <returns>Converted <see cref="Single"/>.</returns>
        public static Single ToSingle(this Runtime.Single measurement)
        {
            return new Single
            {
                Value = measurement.Value,
                Error = measurement.Error
            };
        }

        /// <summary>
        /// Convert from <see cref="Vector2"/> to <see cref="Runtime.Vector2"/>.
        /// </summary>
        /// <param name="vector2"><see cref="Vector2"/> to convert from.</param>
        /// <returns>Converted <see cref="Runtime.Vector2"/>.</returns>
        public static Runtime.Vector2 ToRuntime(this Vector2 vector2)
        {
            return new Runtime.Vector2
            {
                X = vector2.X.ToRuntime(),
                Y = vector2.Y.ToRuntime()
            };
        }

        /// <summary>
        /// Convert from <see cref="Runtime.Vector2"/> to <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector2"><see cref="Runtime.Vector2"/> to convert from.</param>
        /// <returns>Converted <see cref="Vector2"/>.</returns>
        public static Vector2 ToVector2(this Runtime.Vector2 vector2)
        {
            return new Vector2
            {
                X = vector2.X.ToSingle(),
                Y = vector2.Y.ToSingle()
            };
        }

        /// <summary>
        /// Convert from <see cref="Vector3"/> to <see cref="Runtime.Vector3"/>.
        /// </summary>
        /// <param name="vector3"><see cref="Vector3"/> to convert from.</param>
        /// <returns>Converted <see cref="Runtime.Vector3"/>.</returns>
        public static Runtime.Vector3 ToRuntime(this Vector3 vector3)
        {
            return new Runtime.Vector3
            {
                X = vector3.X.ToRuntime(),
                Y = vector3.Y.ToRuntime(),
                Z = vector3.Z.ToRuntime()
            };
        }

        /// <summary>
        /// Convert from <see cref="Runtime.Vector3"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector3"><see cref="Runtime.Vector3"/> to convert from.</param>
        /// <returns>Converted <see cref="Vector3"/>.</returns>
        public static Vector3 ToVector3(this Runtime.Vector3 vector3)
        {
            return new Vector3
            {
                X = vector3.X.ToSingle(),
                Y = vector3.Y.ToSingle(),
                Z = vector3.Z.ToSingle()
            };
        }

        /// <summary>
        /// Convert from <see cref="DataPoint{T}"/> to <see cref="Runtime.DataPoint"/>.
        /// </summary>
        /// <typeparam name="TValue">Type of <see cref="IMeasurement">measurement</see> for the <see cref="DataPoint{T}"/>.</typeparam>
        /// <param name="dataPoint"><see cref="DataPoint{T}"/> to convert from.</param>
        /// <returns>Converted <see cref="Runtime.DataPoint"/>.</returns>
        public static Runtime.DataPoint ToRuntime<TValue>(this DataPoint<TValue> dataPoint)
            where TValue : IMeasurement
        {
            var converted = new Runtime.DataPoint
            {
                TimeSeries = dataPoint.TimeSeries?.ToProtobuf() ?? Guid.Empty.ToProtobuf(),
                Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(dataPoint.Timestamp)
            };

            switch (dataPoint.Measurement)
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
        /// Convert from <see cref="Runtime.DataPoint"/> to <see cref="DataPoint{T}"/>.
        /// </summary>
        /// <param name="dataPoint"><see cref="Runtime.DataPoint"/> to convert from.</param>
        /// <returns>Converted <see cref="DataPoint{T}"/> represented as <see cref="object"/>.</returns>
        public static object ToDataPoint(this Runtime.DataPoint dataPoint)
        {
            Type valueType = typeof(object);
            object valueInstance = null;
            switch (dataPoint.MeasurementCase)
            {
                case Runtime.DataPoint.MeasurementOneofCase.SingleValue:
                    valueType = typeof(Single);
                    valueInstance = dataPoint.SingleValue.ToSingle();
                    break;

                case Runtime.DataPoint.MeasurementOneofCase.Vector2Value:
                    valueType = typeof(Vector2);
                    valueInstance = dataPoint.Vector2Value.ToVector2();
                    break;

                case Runtime.DataPoint.MeasurementOneofCase.Vector3Value:
                    valueType = typeof(Vector3);
                    valueInstance = dataPoint.Vector3Value.ToVector3();
                    break;
            }

            var dataPointType = typeof(DataPoint<>).MakeGenericType(new[] { valueType });
            var dataPointInstance = Activator.CreateInstance(dataPointType);
            var valueProperty = dataPointType.GetProperty("Measurement", BindingFlags.Instance | BindingFlags.Public);
            valueProperty.SetValue(dataPointInstance, valueInstance);

            var timestamp = (Timestamp)dataPoint.Timestamp.ToDateTimeOffset();
            var timestampProperty = dataPointType.GetProperty("Timestamp", BindingFlags.Instance | BindingFlags.Public);
            timestampProperty.SetValue(dataPointInstance, timestamp);

            var timeSeriesProperty = dataPointType.GetProperty("TimeSeries", BindingFlags.Instance | BindingFlags.Public);
            timeSeriesProperty.SetValue(dataPointInstance, dataPoint.TimeSeries.To<TimeSeriesId>());

            return dataPointInstance;
        }
    }
}