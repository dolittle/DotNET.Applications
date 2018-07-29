/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using System.Runtime.Serialization.Formatters;
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

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
            serializerSettings.Converters.Add(new ClrTypeConverter());
            serializerSettings.Converters.Add(new ConceptConverter());
            serializerSettings.Converters.Add(new ConceptDictionaryConverter());

            var configuration = JsonConvert.DeserializeObject<ArtifactsConfiguration>(json, serializerSettings);

            //var configuration = _serializer.FromJson<ArtifactsConfiguration>(json);
            return configuration;
        }

        /// <inheritdoc/>
        public void Save(ArtifactsConfiguration configuration)
        {
            var path = GetPath();

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
            serializerSettings.Converters.Add(new ClrTypeConverter());
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