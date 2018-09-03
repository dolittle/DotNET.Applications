/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifactsConfigurationManager"/>
    /// </summary>
    [Singleton]
    public class ArtifactsConfigurationManager : IArtifactsConfigurationManager
    {
        static string _path = Path.Combine(".dolittle", "artifacts.json");

        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
            serializer =>
            {
                serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
                serializer.Formatting = Formatting.Indented;
            }
        );

        readonly ISerializer _serializer;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactsConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ArtifactsConfigurationManager(ISerializer serializer, ILogger logger)
        {
            _serializer = serializer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public ArtifactsConfiguration Load()
        {
            var path = GetPath();
            if (!File.Exists(path)) return new ArtifactsConfiguration();

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