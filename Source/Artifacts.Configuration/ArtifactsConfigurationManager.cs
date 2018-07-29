/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifactsConfigurationManager"/>
    /// </summary>
    public class ArtifactsConfigurationManager : IArtifactsConfigurationManager
    {
        const string _path   = "../artifacts.json";
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactsConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use</param>
        public ArtifactsConfigurationManager(ISerializer serializer)
        {
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public ArtifactsConfiguration Load()
        {
            var path = GetPath();
            if( !File.Exists(path)) return new ArtifactsConfiguration();

            var json = File.ReadAllText(path);
            var configuration = _serializer.FromJson<ArtifactsConfiguration>(json);
            return configuration;
        }

        /// <inheritdoc/>
        public void Save(ArtifactsConfiguration configuration)
        {
            var path = GetPath();

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            serializerSettings.Converters.Add(new ConceptConverter());

            var json = JsonConvert.SerializeObject(configuration, serializerSettings);
            //_serializer.ToJson(configuration);
            File.WriteAllText(path, json);
        }


        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(),_path);
        }
    }
}