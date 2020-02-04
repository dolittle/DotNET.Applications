// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Exception that gets thrown when the <see cref="BoundedContextConfiguration"/> that's loaded in is invalid.
    /// </summary>
    public class InvalidBoundedContextConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidBoundedContextConfiguration"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public InvalidBoundedContextConfiguration(string description)
            : base(description)
        {
        }
    }
}