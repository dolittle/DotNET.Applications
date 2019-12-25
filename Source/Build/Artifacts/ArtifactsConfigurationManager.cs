// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using Dolittle.Applications;
using Dolittle.Artifacts.Configuration;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifactsConfigurationManager"/>.
    /// </summary>
    [Singleton]
    public class ArtifactsConfigurationManager : IArtifactsConfigurationManager
    {
        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
            serializer =>
            {
                serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
                serializer.Formatting = Formatting.Indented;
            });

        readonly ISerializer _serializer;
        readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactsConfigurationManager"/> class.
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use.</param>
        /// <param name="buildTaskConfiguration">Current <see cref="BuildTaskConfiguration"/>.</param>
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
            return _serializer.FromJson<ArtifactsConfiguration>(json, _serializationOptions);
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