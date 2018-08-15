/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the concept of an Area. The Area of a namespace is the first segment of the Namespace
    /// </summary>
    public class Area : ConceptAs<string>
    {
        /// <summary>
        /// Implicit operator for casting string to <see cref="Area"/>
        /// </summary>
        public static implicit operator Area(string area)
        {
            return new Area(){Value = area};
        }
    }
}