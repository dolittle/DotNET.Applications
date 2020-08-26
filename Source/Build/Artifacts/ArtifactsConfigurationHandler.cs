// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Represents a class that basically handles the interations with a <see cref="ArtifactsConfiguration"/>.
    /// </summary>
    public class ArtifactsConfigurationHandler
    {
        readonly IArtifactsConfigurationManager _configurationManager;
        readonly ArtifactTypes _artifactTypes;
        readonly IBuildMessages _buildMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactsConfigurationHandler"/> class.
        /// </summary>
        /// <param name="configurationManager"><see cref="IArtifactsConfigurationManager"/> for working with the configuration.</param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types.</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for build message output.</param>
        public ArtifactsConfigurationHandler(IArtifactsConfigurationManager configurationManager, ArtifactTypes artifactTypes, IBuildMessages buildMessages)
        {
            _configurationManager = configurationManager;
            _artifactTypes = artifactTypes;
            _buildMessages = buildMessages;
        }

        /// <summary>
        /// Loads the current <see cref="ArtifactsConfiguration"/> from file and uses it to build the updated <see cref="ArtifactsConfiguration"/> using the <see cref="ArtifactsConfigurationBuilder"/>.
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies.</param>
        /// <param name="topology">The <see cref="Applications.Configuration.Topology"/> that's used for building the <see cref="ArtifactsConfiguration"/>.</param>
        /// <param name="configuration">Current <see cref="BuildTaskConfiguration"/>.</param>
        /// <returns>The built <see cref="ArtifactsConfiguration"/>.</returns>
        public ArtifactsConfiguration Build(IEnumerable<Type> types, Applications.Configuration.Topology topology, BuildTaskConfiguration configuration)
        {
            var artifactsConfiguration = _configurationManager.Load();
            var microserviceTopology = new MicroserviceTopology(topology, configuration.UseModules, configuration.NamespaceSegmentsToStrip);
            return new ArtifactsConfigurationBuilder(types, artifactsConfiguration, _artifactTypes, _buildMessages).Build(microserviceTopology);
        }

        /// <summary>
        /// Saves the <see cref="ArtifactsConfiguration"/>.
        /// </summary>
        /// <param name="config"><see cref="ArtifactsConfiguration"/> to save.</param>
        internal void Save(ArtifactsConfiguration config)
        {
            _configurationManager.Save(config);
        }
    }
}