/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dolittle.Build.Artifact
{
    /// <summary>
    /// Represents a class that basically handles the interations with a <see cref="ArtifactsConfiguration"/>
    /// </summary>
    public class ArtifactsConfigurationHandler
    {
        readonly IArtifactsConfigurationManager _configurationManager;
        readonly DolittleArtifactTypes _artifactTypes;
        readonly ILogger _logger;

        
        /// <summary>
        /// Initializes an instance of <see cref="ArtifactsConfigurationHandler"/>
        /// </summary>
        /// <param name="configurationManager"></param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        /// <param name="logger"></param>
        public ArtifactsConfigurationHandler(IArtifactsConfigurationManager configurationManager, DolittleArtifactTypes artifactTypes, ILogger logger)
        {
            _configurationManager = configurationManager;
            _artifactTypes = artifactTypes;
            _logger = logger;
        }

        /// <summary>
        /// Loads the <see cref="ArtifactsConfiguration"/> from file and uses it to build the <see cref="ArtifactsConfiguration"/> using the <see cref="ArtifactsConfigurationBuilder"/>
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies</param>
        /// <param name="topology">The <see cref="Applications.Configuration.Topology"/> that's used for building the <see cref="ArtifactsConfiguration"/></param>
        /// <param name="parsingResults"></param>
        public ArtifactsConfiguration Build(Type[] types, Applications.Configuration.Topology topology, BuildToolArgumentsParsingResult parsingResults)
        {
            var artifactsConfiguration = _configurationManager.Load();
            var boundedContextTopology = new BoundedContextTopology(topology, parsingResults.UseModules, parsingResults.NamespaceSegmentsToStrip);
            return new ArtifactsConfigurationBuilder(types, artifactsConfiguration, _artifactTypes, _logger).Build(boundedContextTopology);
        }
        internal void Save(ArtifactsConfiguration config)
        {
            _configurationManager.Save(config);
        }
    }
}