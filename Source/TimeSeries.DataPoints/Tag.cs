// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the concept of a Tag - something that identifies a time series within a system.
    /// </summary>
    public class Tag : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="Tag"/>.
        /// </summary>
        /// <param name="value">Tag as string.</param>
        public static implicit operator Tag(string value) => new Tag { Value = value };
    }
}