// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the concept of an Area. The Area of a namespace is the first segment of the Namespace.
    /// </summary>
    public class Area : ConceptAs<string>
    {
        /// <summary>
        /// Implicit operator for casting string to <see cref="Area"/>.
        /// </summary>
        /// <param name="area">String representation of area.</param>
        public static implicit operator Area(string area) => new Area() { Value = area };
    }
}