// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Exception that gets thrown when a duplicate artifact is found.
    /// </summary>
    public class DuplicateArtifact : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateArtifact"/> class.
        /// </summary>
        public DuplicateArtifact()
            : base("Duplicate artifacts was found. Are you missing a migrator? ")
        {
        }
    }
}