/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Exception that gets thrown when a duplicate artifact is found
    /// </summary>
    public class DuplicateArtifact : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="DuplicateArtifact"/>
        /// </summary>
        public DuplicateArtifact() : base("Duplicate artifacts was found. Are you missing a migrator? ")
        {
        }

        /// <summary>
        /// Instantiates an instance of <see cref="DuplicateArtifact"/>
        /// </summary>
        public DuplicateArtifact(string message) : base(message)
        {
        }

    }
}