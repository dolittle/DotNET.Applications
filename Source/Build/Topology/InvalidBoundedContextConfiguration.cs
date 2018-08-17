/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Exception that gets thrown when the <see cref="BoundedContextConfiguration"/> that's loaded in is invalid
    /// </summary>
    public class InvalidBoundedContextConfiguration : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="InvalidBoundedContextConfiguration"/>
        /// </summary>
        public InvalidBoundedContextConfiguration(string message)
            : base(message) {}   
    }
}