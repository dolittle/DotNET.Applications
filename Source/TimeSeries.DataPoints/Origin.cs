// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the origin of a datapoint.
    /// </summary>
    public class Origin : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from a <see cref="string"/> representation of origin to <see cref="Origin"/>.
        /// </summary>
        /// <param name="origin"><see cref="string"/> to convert from.</param>
        public static implicit operator Origin(string origin) => new Origin { Value = origin };
    }
}