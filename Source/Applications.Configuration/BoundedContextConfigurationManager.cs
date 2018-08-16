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
        BoundedContextConfiguration _current;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="BoundedContextConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for working with configuration as JSON</param>
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
            var json = _serializer.ToJson(configuration, SerializationOptions.CamelCase);
            File.WriteAllText(path, json);
        }

        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(),_path);
        }
    }
}