/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents the definition of an artifact for configuration
    /// </summary>
    public class ArtifactDefinition
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactDefinition"/>
        /// </summary>
        /// <param name="generation">Generation of the artifact</param>
        /// <param name="type">The CLR type of the arfifact</param>
        public ArtifactDefinition(ArtifactGeneration generation, ClrType type)
        {
            Generation = generation;
            Type = type;
        }

        /*
        /// <summary>
        /// Gets or sets the <see cref="ArtifactId">unique artifact identifier</see>
        /// </summary>
        public ArtifactId Artifact {  get; set; }
        */

        /// <summary>
        /// Gets or sets the <see cref="ArtifactGeneration">generation number</see> for the artifact
        /// </summary>
        public ArtifactGeneration Generation { get; }

        /// <summary>
        /// Gets or sets the <see cref="ClrType"/> represented by the artifact
        /// </summary>
        public ClrType Type { get; }
    }
}