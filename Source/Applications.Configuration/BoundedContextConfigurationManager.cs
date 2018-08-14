/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundedContextConfigurationManager"/>
    /// </summary>
    [Singleton]
    public class BoundedContextConfigurationManager : IBoundedContextConfigurationManager
    {
        const string _path   = "bounded-context.json";
        BoundedContextConfiguration _current;

        /// <inheritdoc/>
        public BoundedContextConfiguration Current 
        { 
            get
            {
                if( _current == null ) _current = Load();
                return _current;
            }
        }

        /// <inheritdoc/>
        public BoundedContextConfiguration Load()
        {
            var path = GetPath();
            if( !File.Exists(path)) throw new MissingBoundedContextConfiguration();

            var json = File.ReadAllText(path);
            var serializerSettings = GetSerializerSettings();
            var configuration = JsonConvert.DeserializeObject<BoundedContextConfiguration>(json, serializerSettings);
            return configuration;
        }

        /// <inheritdoc/>
        public void Save(BoundedContextConfiguration configuration)
        {
            var path = GetPath();
            var serializerSettings = GetSerializerSettings();
            var json = JsonConvert.SerializeObject(configuration);
            File.WriteAllText(path, json);
        }

        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(),_path);
        }

        JsonSerializerSettings GetSerializerSettings()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
            serializerSettings.Converters.Add(new ConceptConverter());
            serializerSettings.Converters.Add(new ConceptDictionaryConverter());
            return serializerSettings;
        }
    }
}