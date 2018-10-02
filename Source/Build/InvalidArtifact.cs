/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Build
{
    /// <summary>
    /// The exception that gets thrown when an invalid artifact is discovered
    /// </summary>
    public class InvalidArtifact : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="InvalidArtifact"/>
        /// </summary>
        public InvalidArtifact() 
            : base("Invalid Artifacts was discovered") 
        { }

        /// <summary>
        /// Instantiates an instance of <see cref="InvalidArtifact"/>
        /// </summary>
        public InvalidArtifact(Type type)
            : base($"Artifact {type.Name} with namespace = {type.Namespace} is invalid")
        { }

        /// <summary>
        /// Instantiates an instance of <see cref="InvalidArtifact"/>
        /// </summary>
        public InvalidArtifact(string typePath)
            : base($"Artifact with type path (a Module name + Feature names composition) {typePath} is invalid")
        { }
    }
}