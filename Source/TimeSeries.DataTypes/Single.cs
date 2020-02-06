// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

#pragma warning disable CA1720

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// Represents a measurement.
    /// </summary>
    public class Single : Value<Single>, IMeasurement
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the measurement error.
        /// </summary>
        /// <remarks>
        /// Typicaly the value is of a number, an error of 0 would mean there is
        /// no deviations to be expected from the value - the value is 100% accurate.
        /// </remarks>
        public double Error { get; set; }

        /// <summary>
        /// Implicitly convert from the a value of the type the <see cref="Single"/> is
        /// for to an instance with the value.
        /// </summary>
        /// <param name="value">Value to convert from.</param>
        /// <remarks>
        /// The <see cref="Error">error property</see> will be set to.
        /// </remarks>
        public static implicit operator Single(double value) => new Single { Value = value, Error = 0 };
    }
}