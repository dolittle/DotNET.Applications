using System;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Logging;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// 
    /// </summary>
    public class ProxiesHandler
    {
        readonly TemplateLoader _templateLoader;
        readonly DolittleArtifactTypes _artifactTypes;
        readonly ILogger _logger;
        
        public ProxiesHandler(TemplateLoader templateLoader, DolittleArtifactTypes artifactTypes, ILogger logger)
        {
            _templateLoader = templateLoader;
            _artifactTypes = artifactTypes;
            _logger = logger;
        }

        /// <summary>
        /// Creates the proxies given a list of artifacts and configurations
        /// </summary>
        /// <param name="artifacts"></param>
        /// <param name="boundedContextConfiguration"></param>
        /// <param name="artifactsConfiguration"></param>
        public void CreateProxies(Type[] artifacts, BoundedContextConfiguration boundedContextConfiguration, ArtifactsConfiguration artifactsConfiguration)
        {
            var builder = new ProxiesBuilder(_templateLoader, artifacts, _artifactTypes, _logger);
            builder.Build(artifactsConfiguration, boundedContextConfiguration);
        }
    }
}