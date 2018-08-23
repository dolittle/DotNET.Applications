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
        public void GenerateProxies(ArtifactsConfiguration artifactsConfiguration, BoundedContextConfiguration boundedContextConfiguration)
        {
            _logger.Information("Building proxies");
            var startTime = DateTime.UtcNow;
            var proxies = new List<Proxy>();

            CreateCommands(artifactsConfiguration, boundedContextConfiguration, ref proxies);

            WriteProxiesToFile(proxies.ToArray());
            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished proxies build process. (Took {deltaTime.TotalSeconds} seconds)");
        }

        void CreateCommands(ArtifactsConfiguration artifactsConfig, BoundedContextConfiguration boundedContextConfiguration, ref List<Proxy> proxies)
        {
            var template = _templateLoader.CommandProxyTemplate;

            var commandArtifactType = _artifactTypes.ArtifactTypes.Single(_ => _.TypeName.Equals("command")).Type;
            var commandArtifacts = _artifacts.Where(_ => commandArtifactType.IsAssignableFrom(_));

            foreach (var artifact in commandArtifacts)
                GenerateCommandProxy(artifact, artifactsConfig, boundedContextConfiguration, template, ref proxies);
            
        }

        void GenerateCommandProxy(Type artifact, ArtifactsConfiguration artifactsConfig, BoundedContextConfiguration boundedContextConfiguration, Func<object, string> template, ref List<Proxy> proxies)
        {
            var artifactId = GetArtifactId(artifact, artifactsConfig);

            if (artifact.HasVisibleProperties())
            {
                _logger.Information($"Creating command proxy for {ClrType.FromType(artifact).TypeString}");
                var propertiesInfo = artifact.GetProperties();

                var handlebarsCommand = new HandlebarsCommand()
                {
                    CommandName = artifact.Name,
                    ArtifactId = artifactId.Value.ToString()
                };
                AddCommandProxyProperties(propertiesInfo, ref handlebarsCommand);
                var fileContent = template(handlebarsCommand);
                var filePath = GenerateFilePath(artifact, boundedContextConfiguration, artifact.Name);

                proxies.Add(new Proxy(){Content = fileContent, FullFilePath = filePath});
            }
            else
            {
                _logger.Information($"No visible properties for {ClrType.FromType(artifact).TypeString}");
            }
        }

        string GenerateFilePath(Type artifact, BoundedContextConfiguration config, string artifactName)
        {
            var @namespace = artifact.StripExcludedNamespaceSegments(config);
            return @namespace.Replace('.', '/') + "/" + artifactName+ ".js";
        }

        void AddCommandProxyProperties(PropertyInfo[] properties, ref HandlebarsCommand command)
        {
            foreach (var prop in properties)
            {
                var defaultValue = prop.PropertyType.GetDefaultValue();
                
                var proxyProperty = new ProxyProperty()
                {
                    PropertyName = prop.Name.ToCamelCase(),
                    PropertyDefaultValue = TranslateDefaultValueToJavascript(defaultValue)
                };
                command.Properties.Add(proxyProperty);
            }
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
        object TranslateDefaultValueToJavascript(object defaultValue)
        {
                if (defaultValue is string) defaultValue = $"'{defaultValue}'";
                else if (defaultValue == null) defaultValue = "null";

                return defaultValue;
        }
        ArtifactId GetArtifactId(Type artifact, ArtifactsConfiguration config)
        {
            return config.GetMatchingArtifactDefinition(artifact).Artifact;
        }
    }
}