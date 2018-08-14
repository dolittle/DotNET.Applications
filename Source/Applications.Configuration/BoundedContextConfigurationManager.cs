/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Execution;
using Dolittle.Serialization.Json;
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
        readonly ISerializer _serializer;
        BoundedContextConfiguration _current;

        /// <summary>
        /// Initializes a new instance of <see cref="BoundedContextConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"></param>
        public BoundedContextConfigurationManager(ISerializer serializer)
        {
            _serializer = serializer;
        }

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
            var configuration = _serializer.FromJson<BoundedContextConfiguration>(json);
            return configuration;
        }

        /// <inheritdoc/>
        public void Save(BoundedContextConfiguration configuration)
        {
            var path = GetPath();

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
            serializerSettings.Converters.Add(new ConceptConverter());
            var json = JsonConvert.SerializeObject(configuration, serializerSettings);
            File.WriteAllText(path, json);
        }

        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(),_path);
        }
    }
}