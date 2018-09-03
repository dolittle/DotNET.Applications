/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundedContextConfigurationManager"/>
    /// </summary>
    [Singleton]
    public class BoundedContextConfigurationManager : IBoundedContextConfigurationManager
    {
        static string _path   = Path.Combine(".dolittle","bounded-context.json");
        BoundedContextConfiguration _current;
        readonly ISerializer _serializer;
        readonly ILogger _logger;

        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
            serializer =>
            {
                serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
                serializer.Formatting = Formatting.Indented;
            }
        );
        /// <summary>
        /// Initializes a new instance of <see cref="BoundedContextConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for working with configuration as JSON</param>
        /// <param name="logger"></param>
        public BoundedContextConfigurationManager(ISerializer serializer, ILogger logger)
        {
            _serializer = serializer;
            _logger = logger;
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
            if( !File.Exists(path)) throw new MissingBoundedContextConfiguration(_path);
            
            var json = File.ReadAllText(path);
            var configuration = _serializer.FromJson<BoundedContextConfiguration>(json, _serializationOptions);
            return configuration;
        }

        /// <inheritdoc/>
        public void Save(BoundedContextConfiguration configuration)
        {
            var path = GetPath();
            if( !File.Exists(path)) throw new MissingBoundedContextConfiguration(_path);
            var json = _serializer.ToJson(configuration, _serializationOptions);
            
            File.WriteAllText(path, json);
        }

        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _path);
        }
    }
}