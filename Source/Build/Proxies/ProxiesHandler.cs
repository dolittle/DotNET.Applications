/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a class that handles the interaction with the proxy builder
    /// </summary>
    public class ProxiesHandler
    {
        readonly TemplateLoader _templateLoader;
        readonly ArtifactTypes _artifactTypes;
        readonly IBuildMessages _buildMessages;

        /// <summary>
        /// Instantiates a new instance of <see cref="ProxiesHandler"/>
        /// </summary>
        /// <param name="templateLoader"></param>
        /// <param name="artifactTypes"></param>
        /// <param name="buildMessages"></param>
        public ProxiesHandler(TemplateLoader templateLoader, ArtifactTypes artifactTypes, IBuildMessages buildMessages)
        {
            _templateLoader = templateLoader;
            _artifactTypes = artifactTypes;
            _buildMessages = buildMessages;
        }

        /// <summary>
        /// Creates the proxies given a list of artifacts and configurations
        /// </summary>
        /// <param name="artifacts"></param>
        /// <param name="configuration"></param>
        /// <param name="artifactsConfiguration"></param>
        public void CreateProxies(Type[] artifacts, BuildTaskConfiguration configuration, ArtifactsConfiguration artifactsConfiguration)
        {
            var builder = new ProxiesBuilder(_templateLoader, artifacts, _artifactTypes, _buildMessages);
            builder.GenerateProxies(artifactsConfiguration, configuration);
        }
    }
}