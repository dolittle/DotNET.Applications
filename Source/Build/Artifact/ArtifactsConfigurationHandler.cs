/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Logging;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Artifact
{
    /// <summary>
    /// Represents a class that basically handles the interations with a <see cref="ArtifactsConfiguration"/>
    /// </summary>
    public class ArtifactsConfigurationHandler
    {
        readonly ArtifactsConfigurationManager _configurationManager;
        readonly IEnumerable<ArtifactType> _artifactTypes;
        
        /// <summary>
        /// Initializes an instance of <see cref="ArtifactsConfigurationHandler"/>
        /// </summary>
        /// <param name="configurationSerializer">The serializer for the <see cref="ArtifactsConfigurationManager"/></param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        public ArtifactsConfigurationHandler(ISerializer configurationSerializer, IEnumerable<ArtifactType> artifactTypes)
        {
            _configurationManager = new ArtifactsConfigurationManager(configurationSerializer);
            _artifactTypes = artifactTypes;
        }

        /// <summary>
        /// Loads the <see cref="ArtifactsConfiguration"/> from file and uses it to build the <see cref="ArtifactsConfiguration"/> using the <see cref="ArtifactsConfigurationBuilder"/>
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies</param>
        /// <param name="boundedContextConfiguration">The <see cref="BoundedContextConfiguration"/> that's used for building the <see cref="ArtifactsConfiguration"/></param>
        /// <param name="logger"></param>
        public ArtifactsConfiguration Build(Type[] types, BoundedContextConfiguration boundedContextConfiguration, ILogger logger)
        {
            var artifactsConfiguration = _configurationManager.Load();
            return new ArtifactsConfigurationBuilder(types, artifactsConfiguration, _artifactTypes, logger).Build(boundedContextConfiguration);
        }
        internal void Save(ArtifactsConfiguration config)
        {
            _configurationManager.Save(config);
        }
    }
}