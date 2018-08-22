/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Logging;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// 
    /// </summary>
     public class ProxiesBuilder
    {
        readonly Type[] _artifacts;
        readonly ArtifactsConfiguration _artifactsConfiguration;
        readonly BoundedContextConfiguration _boundedContextConfiguration;
        readonly ArtifactType[] _artifactTypes;
        readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies</param>
        /// <param name="artifactsConfiguration"></param>
        /// <param name="boundedContextConfiguration"></param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        /// <param name="logger"></param>
        public ProxiesBuilder(Type[] artifacts, ArtifactsConfiguration artifactsConfiguration, BoundedContextConfiguration boundedContextConfiguration, ArtifactType[] artifactTypes, ILogger logger)
        {
            _artifacts = artifacts;
            _artifactsConfiguration = artifactsConfiguration;
            _boundedContextConfiguration = boundedContextConfiguration; 
            _artifactTypes = artifactTypes;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Build()
        {
            _logger.Information("Building proxies");
            var startTime = DateTime.UtcNow;
            CreateCommands();
            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished proxies build process. (Took {deltaTime.TotalSeconds} seconds)");
        }

        void CreateCommands()
        {
            var commandArtifactType = _artifactTypes.Single(_ => _.TypeName.Equals("command")).Type;
            var commandArtifacts = _artifacts.Where(_ => commandArtifactType.IsAssignableFrom(_));

            
        }
    }
}