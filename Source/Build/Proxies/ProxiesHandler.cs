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
        readonly DolittleArtifactTypes _artifactTypes;
        readonly IBuildToolLogger _logger;

        /// <summary>
        /// Instantiates a new instance of <see cref="ProxiesHandler"/>
        /// </summary>
        /// <param name="templateLoader"></param>
        /// <param name="artifactTypes"></param>
        /// <param name="logger"></param>
        public ProxiesHandler(TemplateLoader templateLoader, DolittleArtifactTypes artifactTypes, IBuildToolLogger logger)
        {
            _templateLoader = templateLoader;
            _artifactTypes = artifactTypes;
            _logger = logger;
        }

        /// <summary>
        /// Creates the proxies given a list of artifacts and configurations
        /// </summary>
        /// <param name="artifacts"></param>
        /// <param name="parsingResults"></param>
        /// <param name="artifactsConfiguration"></param>
        public void CreateProxies(Type[] artifacts, ArgumentsParsingResult parsingResults, ArtifactsConfiguration artifactsConfiguration)
        {
            var builder = new ProxiesBuilder(_templateLoader, artifacts, _artifactTypes, _logger);
            builder.GenerateProxies(artifactsConfiguration, parsingResults);
        }
    }
}