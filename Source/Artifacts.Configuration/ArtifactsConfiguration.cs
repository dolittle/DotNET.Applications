/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Applications;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents the definition of features for configuration
    /// </summary>
    public class ArtifactsConfiguration
    {
        /// <summary>
        /// Gets or sets the dictionary of <see cref="ArtifactsByTypeDefinition"/> per <see cref="Feature"/>
        /// </summary>
        public Dictionary<Feature, ArtifactsByTypeDefinition>  Artifacts { get; set; } = new Dictionary<Feature, ArtifactsByTypeDefinition>();
    }
}