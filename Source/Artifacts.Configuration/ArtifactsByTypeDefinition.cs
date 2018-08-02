/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents the definition of artifacts grouped by the different types for configuration
    /// </summary>
    public class ArtifactsByTypeDefinition
    {
        /// <summary>
        /// Gets or sets the Command artifacts
        /// </summary>
        public IEnumerable<ArtifactDefinition> Commands {  get; set; } = new ArtifactDefinition[0];

        /// <summary>
        /// Gets or sets the Event artifacts
        /// </summary>
        public IEnumerable<ArtifactDefinition> Events {  get; set; } = new ArtifactDefinition[0];

        /// <summary>
        /// Gets or sets the EventProcessor artifacts
        /// </summary>
        public IEnumerable<ArtifactDefinition> EventProcessors {  get; set; } = new ArtifactDefinition[0];

        /// <summary>
        /// Gets or sets the EventSource artifacts
        /// </summary>
        public IEnumerable<ArtifactDefinition> EventSources {  get; set; } = new ArtifactDefinition[0];

        /// <summary>
        /// Gets or sets the ReadModels artifacts
        /// </summary>
        public IEnumerable<ArtifactDefinition> ReadModels {  get; set; } = new ArtifactDefinition[0];

        /// <summary>
        /// Gets or sets the Queries artifacts
        /// </summary>
        public IEnumerable<ArtifactDefinition> Queries {  get; set; } = new ArtifactDefinition[0];
    }
}