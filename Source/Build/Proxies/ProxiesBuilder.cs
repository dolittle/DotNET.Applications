/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Artifact;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Queries;
using Dolittle.Reflection;
using Dolittle.Strings;
using HandlebarsDotNet;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a class that handles the generation of proxies
    /// </summary>
    public class ProxiesBuilder
    {
        readonly TemplateLoader _templateLoader;
        readonly Type[] _artifacts;
        readonly DolittleArtifactTypes _artifactTypes;
        readonly ILogger _logger;

        /// <summary>
        /// Instantiates an instance of <see cref="ProxiesBuilder"/>
        /// </summary>
        /// <param name="templateLoader"></param>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies</param>
        /// <param name="artifactTypes"></param>
        /// <param name="logger"></param>
        public ProxiesBuilder(TemplateLoader templateLoader, Type[] artifacts, DolittleArtifactTypes artifactTypes, ILogger logger)
        {
            _templateLoader = templateLoader;
            _artifacts = artifacts;
            _artifactTypes = artifactTypes;
            _logger = logger;
        }
        /// <summary>
        /// Generates all proxies for all relevant artifacts and writes them as files in their corresponding feature structure
        /// </summary>
        public void GenerateProxies(ArtifactsConfiguration artifactsConfiguration, BuildToolArgumentsParsingResult parsingResults)
        {
            _logger.Information("Building proxies");
            var startTime = DateTime.UtcNow;
            var proxies = new List<Proxy>();

            GenerateProxies(artifactsConfiguration, parsingResults, _templateLoader.CommandProxyTemplate, "command", GenerateCommandProxy, ref proxies);
            GenerateProxies(artifactsConfiguration, parsingResults, _templateLoader.QueryProxyTemplate, "query", GenereateQueryProxy, ref proxies);
            GenerateProxies(artifactsConfiguration, parsingResults, _templateLoader.ReadModelProxyTemplate, "read model", GenerateReadModelProxy, ref proxies);
            WriteProxiesToFile(proxies.ToArray());
            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished proxies build process. (Took {deltaTime.TotalSeconds} seconds)");
        }

        void GenerateProxies(
            ArtifactsConfiguration artifactsConfiguration, 
            BuildToolArgumentsParsingResult parsingResults, 
            Func<object, string> template, 
            string artifactTypeName, 
            Func<Type, ArtifactsConfiguration, BuildToolArgumentsParsingResult, Func<object, string>, Proxy> ProxyGeneratorFunction,
            ref List<Proxy> proxies)
        {
            var artifactType = _artifactTypes.ArtifactTypes.Single(_ => _.TypeName.Equals(artifactTypeName)).Type;
            var artifacts = _artifacts.Where(_ => artifactType.IsAssignableFrom(_));

            foreach (var artifact in artifacts)
            {
                var proxy = ProxyGeneratorFunction(artifact, artifactsConfiguration, parsingResults, template);
                if (proxy !=  null) proxies.Add(proxy);
            }
        }
        Proxy GenerateCommandProxy(Type artifact, ArtifactsConfiguration artifactsConfig, BuildToolArgumentsParsingResult parsingResults, Func<object, string> template)
        {
            var artifactId = GetArtifactId(artifact, artifactsConfig);

            if (artifact.HasVisibleProperties())
            {
                _logger.Trace($"Creating command proxy for {ClrType.FromType(artifact).TypeString}");
                var propertiesInfo = artifact.GetProperties();

                var handlebarsCommand = new HandlebarsCommand()
                {
                    CommandName = artifact.Name,
                    ArtifactId = artifactId.Value.ToString()
                };
                handlebarsCommand.Properties = CreateProxyProperties(propertiesInfo);
                return CreateProxy(artifact, template(handlebarsCommand), parsingResults);
            }
            else
            {
                _logger.Trace($"No visible properties for {ClrType.FromType(artifact).TypeString}");
                return null;
            }
        }
        Proxy GenereateQueryProxy(Type artifact, ArtifactsConfiguration artifactsConfig, BuildToolArgumentsParsingResult parsingResults, Func<object, string> template)
        {
            _logger.Trace($"Creating query proxy for {ClrType.FromType(artifact).TypeString}");
            var handlebarsQuery = new HandlebarsQuery()
            {
                ClrType = artifact.FullName,
                QueryName = artifact.Name
            };
            var setableProperties = artifact.GetSettableProperties();
            if (setableProperties.Any())
                handlebarsQuery.Properties = CreateProxyProperties(setableProperties);
            
            return CreateProxy(artifact, template(handlebarsQuery), parsingResults);
            
        }
        Proxy GenerateReadModelProxy(Type artifact, ArtifactsConfiguration artifactsConfig, BuildToolArgumentsParsingResult parsingResults, Func<object, string> template)
        {
            _logger.Trace($"Creating read model proxy for {ClrType.FromType(artifact).TypeString}");
            var artifactDefinition = GetArtifactDefinition(artifact, artifactsConfig);
            var handlebarsReadmodel = new HandlebarsReadmodel()
            {
                ReadModelName = artifact.Name,
                ReadModelArtifactId = artifactDefinition.Artifact.Value.ToString(),
                ReadModelGeneration = artifactDefinition.Generation.Value.ToString()
            };
            var setableProperties = artifact.GetSettableProperties();
            if (setableProperties.Any())
                handlebarsReadmodel.Properties = CreateProxyProperties(setableProperties);
            
            return CreateProxy(artifact, template(handlebarsReadmodel), parsingResults);
            
        }

        string GenerateFilePath(Type artifact, BuildToolArgumentsParsingResult parsingResults, string artifactName)
        {
            var @namespace = artifact.StripExcludedNamespaceSegments(parsingResults);
            return Path.Join(parsingResults.ProxiesBasePath, @namespace.Replace('.', '/'), $"{artifactName}.js");
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

        Proxy CreateProxy(Type artifact, string fileContent, BuildToolArgumentsParsingResult parsingResults)
        {
            var filePath = GenerateFilePath(artifact, parsingResults, artifact.Name);

            return (new Proxy(){Content = fileContent, FullFilePath = filePath});
        }
        void WriteProxiesToFile(Proxy[] proxies)
        {
            foreach (var proxy in proxies)
            {
                var fileInfo = new FileInfo(proxy.FullFilePath);
                if (! fileInfo.Directory.Exists) System.IO.Directory.CreateDirectory(fileInfo.DirectoryName);
                using (var streamWriter = new StreamWriter(proxy.FullFilePath, false, Encoding.UTF8, 65536))
                {
                    streamWriter.Write(proxy.Content);
                }
            }
        }
        ArtifactId GetArtifactId(Type artifact, ArtifactsConfiguration config)
        {
            return GetArtifactDefinition(artifact, config).Artifact;
        }
        ArtifactDefinition GetArtifactDefinition(Type artifact, ArtifactsConfiguration config)
        {
            return config.GetMatchingArtifactDefinition(artifact);
        }
    }
}