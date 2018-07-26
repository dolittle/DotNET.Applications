/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents the definition of an artifact
    /// </summary>
    public class ArtifactDefinition
    {
        /// <summary>
        /// Gets or sets the <see cref="ArtifactId">unique artifact identifier</see>
        /// </summary>
        public ArtifactId Artifact { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ArtifactGeneration">generation number</see> for the artifact
        /// </summary>
        public ArtifactGeneration   Generation { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> represented by the artifact
        /// </summary>
        public Type Type { get; set; }
    }
}