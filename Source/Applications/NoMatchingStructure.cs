/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception that gets thrown when there is no matching structure for a set of types
    /// </summary>
    public class NoMatchingStructure : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NoMatchingStructure"/>
        /// </summary>
        /// <param name="types"><see cref="IEnumerable{Type}">Types</see> without match</param>
        public NoMatchingStructure(IEnumerable<Type> types) : base($"Couldn't find match in the application structure for any of the type(s) '{string.Join(",",types.Select(type => type.AssemblyQualifiedName))}'")
        {
        }
    }
}