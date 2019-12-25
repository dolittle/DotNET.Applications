// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build
{
    /// <summary>
    /// The exception that gets thrown when an invalid artifact is discovered.
    /// </summary>
    public class InvalidArtifact : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArtifact"/> class.
        /// </summary>
        public InvalidArtifact()
            : base("Invalid Artifacts was discovered")
        {
        }
    }
}