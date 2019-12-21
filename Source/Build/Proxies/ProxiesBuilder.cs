// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Artifacts;
using Dolittle.Reflection;
using Dolittle.Strings;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a class that handles the generation of proxies.
    /// </summary>
    public class ProxiesBuilder
    {
        readonly TemplateLoader _templateLoader;
        readonly IEnumerable<Type> _artifacts;
        readonly ArtifactTypes _artifactTypes;
        readonly IBuildMessages _buildMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxiesBuilder"/> class.
        /// </summary>
        /// <param name="templateLoader"><see cref="TemplateLoader"/>.</param>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies.</param>
        /// <param name="artifactTypes">All <see cref="ArtifactTypes"/>.</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting build messages.</param>
        public ProxiesBuilder(TemplateLoader templateLoader, IEnumerable<Type> artifacts, ArtifactTypes artifactTypes, IBuildMessages buildMessages)
        {
            _templateLoader = templateLoader;
            _artifacts = artifacts;
            _artifactTypes = artifactTypes;
            _buildMessages = buildMessages;
        }

        /// <summary>
        /// Generates all proxies for all relevant artifacts and writes them as files in their corresponding feature structure.
        /// </summary>
        /// <param name="artifactsConfiguration">The <see cref="ArtifactsConfiguration"/>.</param>
        /// <param name="configuration">The <see cref="BuildTaskConfiguration"/>.</param>
        public void GenerateProxies(ArtifactsConfiguration artifactsConfiguration, BuildTaskConfiguration configuration)
        {
            var proxies = new List<Proxy>();

            proxies.AddRange(GenerateProxies(artifactsConfiguration, configuration, _templateLoader.CommandProxyTemplate, "command", GenerateCommandProxy));
            proxies.AddRange(GenerateProxies(artifactsConfiguration, configuration, _templateLoader.QueryProxyTemplate, "query", GenereateQueryProxy));
            proxies.AddRange(GenerateProxies(artifactsConfiguration, configuration, _templateLoader.ReadModelProxyTemplate, "read model", GenerateReadModelProxy));
            WriteProxiesToFile(proxies.ToArray());
        }

        IEnumerable<Proxy> GenerateProxies(
            ArtifactsConfiguration artifactsConfiguration,
            BuildTaskConfiguration configuration,
            Func<object, string> template,
            string artifactTypeName,
            Func<Type, ArtifactsConfiguration, BuildTaskConfiguration, Func<object, string>, IEnumerable<Proxy>> proxyGeneratorFunction)
        {
            List<Proxy> proxies = new List<Proxy>();
            var artifactType = _artifactTypes.Single(_ => _.TypeName.Equals(artifactTypeName, StringComparison.InvariantCulture)).Type;
            foreach (var artifact in _artifacts.Where(_ => artifactType.IsAssignableFrom(_)))
            {
                var newProxies = proxyGeneratorFunction(artifact, artifactsConfiguration, configuration, template);
                proxies.AddRange(newProxies);
            }

            return proxies;
        }

        IEnumerable<Proxy> GenerateCommandProxy(Type artifact, ArtifactsConfiguration artifactsConfig, BuildTaskConfiguration configuration, Func<object, string> template)
        {
            _buildMessages.Trace($"Creating command proxy for {ClrType.FromType(artifact).TypeString}");

            var artifactId = GetArtifactId(artifact, artifactsConfig);
            var handlebarsCommand = new HandlebarsCommand()
            {
                CommandName = artifact.Name,
                ArtifactId = artifactId.Value.ToString()
            };
            var setableProperties = artifact.GetSettableProperties();

            if (setableProperties.Length > 0)
                handlebarsCommand.Properties = CreateProxyProperties(setableProperties);

            var proxies = new List<Proxy>();
            foreach (var path in configuration.ProxiesBasePath)
            {
                proxies.Add(CreateProxy(artifact, template(handlebarsCommand), configuration, path));
            }

            return proxies;
        }

        IEnumerable<Proxy> GenereateQueryProxy(Type artifact, ArtifactsConfiguration artifactsConfig, BuildTaskConfiguration configuration, Func<object, string> template)
        {
            _buildMessages.Trace($"Creating query proxy for {ClrType.FromType(artifact).TypeString}");
            var handlebarsQuery = new HandlebarsQuery()
            {
                ClrType = artifact.FullName,
                QueryName = artifact.Name
            };
            var settableProperties = artifact.GetSettableProperties();

            if (settableProperties.Length > 0)
                handlebarsQuery.Properties = CreateProxyProperties(settableProperties);

            var proxies = new List<Proxy>();
            foreach (var path in configuration.ProxiesBasePath)
            {
                proxies.Add(CreateProxy(artifact, template(handlebarsQuery), configuration, path));
            }

            return proxies;
        }

        IEnumerable<Proxy> GenerateReadModelProxy(Type artifact, ArtifactsConfiguration artifactsConfig, BuildTaskConfiguration configuration, Func<object, string> template)
        {
            _buildMessages.Trace($"Creating read model proxy for {ClrType.FromType(artifact).TypeString}");
            var artifactId = GetArtifactId(artifact, artifactsConfig);
            var artifactDefinition = GetArtifactDefinition(artifact, artifactsConfig);
            var handlebarsReadmodel = new HandlebarsReadmodel()
            {
                ReadModelName = artifact.Name,
                ReadModelArtifactId = artifactId.Value.ToString(),
                ReadModelGeneration = artifactDefinition.Generation.Value.ToString(CultureInfo.InvariantCulture)
            };

            var settableProperties = artifact.GetSettableProperties();
            if (settableProperties.Length > 0)
                handlebarsReadmodel.Properties = CreateProxyProperties(settableProperties);

            var proxies = new List<Proxy>();
            foreach (var path in configuration.ProxiesBasePath)
            {
                proxies.Add(CreateProxy(artifact, template(handlebarsReadmodel), configuration, path));
            }

            return proxies;
        }

        string GenerateFilePath(Type artifact, BuildTaskConfiguration configuration, string artifactName, string proxyBasePath)
        {
            var @namespace = artifact.StripExcludedNamespaceSegments(configuration);
            return Path.Combine(proxyBasePath, @namespace.Replace('.', '/'), $"{artifactName}.js");
        }

        IEnumerable<ProxyProperty> CreateProxyProperties(PropertyInfo[] properties)
        {
            var proxyProperties = new List<ProxyProperty>();
            foreach (var prop in properties)
            {
                var defaultValue = prop.PropertyType.GetDefaultValueAsString();

                var proxyProperty = new ProxyProperty()
                {
                    PropertyName = prop.Name.ToCamelCase(),
                    PropertyDefaultValue = defaultValue
                };
                proxyProperties.Add(proxyProperty);
            }

            return proxyProperties;
        }

        Proxy CreateProxy(Type artifact, string fileContent, BuildTaskConfiguration configuration, string proxyBasePath)
        {
            var filePath = GenerateFilePath(artifact, configuration, artifact.Name, proxyBasePath);

            return new Proxy() { Content = fileContent, FullFilePath = filePath };
        }

        void WriteProxiesToFile(Proxy[] proxies)
        {
            foreach (var proxy in proxies)
            {
                var fileInfo = new FileInfo(proxy.FullFilePath);
                if (!fileInfo.Directory.Exists) System.IO.Directory.CreateDirectory(fileInfo.DirectoryName);
                using (var streamWriter = new StreamWriter(proxy.FullFilePath, false, Encoding.UTF8))
                {
                    streamWriter.Write(proxy.Content);
                }
            }
        }

        ArtifactId GetArtifactId(Type artifact, ArtifactsConfiguration config)
        {
            return config.GetMatchingArtifactId(artifact);
        }

        ArtifactDefinition GetArtifactDefinition(Type artifact, ArtifactsConfiguration config)
        {
            return config.GetMatchingArtifactDefinition(artifact);
        }
    }
}