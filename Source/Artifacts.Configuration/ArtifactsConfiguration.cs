/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Configuration;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents the definition of features for configuration
    /// </summary>
    [Name("artifacts")]
    public class ArtifactsConfiguration : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactsConfiguration"/>
        /// </summary>
        /// <param name="artifacts"><see cref="IDictionary{TKey, TValue}"/> for artifacts per feature</param>
        public ArtifactsConfiguration(IDictionary<Feature, ArtifactsByTypeDefinition> artifacts)
        {
            Artifacts = artifacts;
        }

        /// <summary>
        /// Gets the dictionary of <see cref="ArtifactsByTypeDefinition"/> per <see cref="Feature"/>
        /// </summary>
        public IDictionary<Feature, ArtifactsByTypeDefinition>  Artifacts { get; }
    }
}