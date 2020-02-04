// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Artifacts.Configuration;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a class that handles the interaction with the proxy builder.
    /// </summary>
    public class ProxiesHandler
    {
        readonly TemplateLoader _templateLoader;
        readonly ArtifactTypes _artifactTypes;
        readonly IBuildMessages _buildMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxiesHandler"/> class.
        /// </summary>
        /// <param name="templateLoader"><see cref="TemplateLoader"/> for loading templates.</param>
        /// <param name="artifactTypes">All <see cref="ArtifactTypes"/>.</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting build messages.</param>
        public ProxiesHandler(TemplateLoader templateLoader, ArtifactTypes artifactTypes, IBuildMessages buildMessages)
        {
            _templateLoader = templateLoader;
            _artifactTypes = artifactTypes;
            _buildMessages = buildMessages;
        }

        /// <summary>
        /// Creates the proxies given a list of artifacts and configurations.
        /// </summary>
        /// <param name="artifacts">All artifacts as types.</param>
        /// <param name="configuration">Current <see cref="BuildTaskConfiguration"/>.</param>
        /// <param name="artifactsConfiguration">Current <see cref="ArtifactsConfiguration"/>.</param>
        public void CreateProxies(IEnumerable<Type> artifacts, BuildTaskConfiguration configuration, ArtifactsConfiguration artifactsConfiguration)
        {
            var builder = new ProxiesBuilder(_templateLoader, artifacts, _artifactTypes, _buildMessages);
            builder.GenerateProxies(artifactsConfiguration, configuration);
        }
    }
}