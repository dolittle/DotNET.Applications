/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Dolittle.Artifacts.Configuration;
using Dolittle.Applications;
using System.Collections.Generic;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifactsConfigurationManager"/>
    /// </summary>
    [Singleton]
    public class ArtifactsConfigurationManager : IArtifactsConfigurationManager
    {

        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
            serializer =>
            {
                serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
                serializer.Formatting = Formatting.Indented;
            }
        );

        readonly ISerializer _serializer;
        readonly string _path;

        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactsConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use</param>
        /// <param name="buildTaskConfiguration">Current <see cref="BuildTaskConfiguration"/></param>
        public ArtifactsConfigurationManager(
            ISerializer serializer,
            BuildTaskConfiguration buildTaskConfiguration)
        {
            _serializer = serializer;
            _path = Path.Combine(buildTaskConfiguration.DolittleFolder, "artifacts.json");
        }


        /// <inheritdoc/>
        public ArtifactsConfiguration Load()
        {
            var path = GetPath();
            if (!File.Exists(path)) return new ArtifactsConfiguration(new Dictionary<Feature, ArtifactsByTypeDefinition>());

            var json = File.ReadAllText(path);
            var configuration = _serializer.FromJson<ArtifactsConfiguration>(json, _serializationOptions);
            return configuration;
        }

        /// <inheritdoc/>
        public void Save(ArtifactsConfiguration configuration)
        {
            var path = GetPath();

            var json = _serializer.ToJson(configuration, _serializationOptions);
            File.WriteAllText(path, json);
        }

        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _path);
        }
    }
}