/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Dolittle.Applications.Configuration;
using System.Collections.Generic;
using Dolittle.Applications;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents an implementation of <see cref="ITopologyConfigurationManager"/>
    /// </summary>
    [Singleton]
    public class TopologyConfigurationManager : ITopologyConfigurationManager
    {
        readonly static string _path = Path.Combine(".dolittle", "topology.json");
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
        /// Initializes a new instance of <see cref="TopologyConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for working with configuration as JSON</param>
        /// <param name="logger"></param>
        public TopologyConfigurationManager(ISerializer serializer, ILogger logger)
        {
            _serializer = serializer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Dolittle.Applications.Configuration.Topology Load()
        {
            var path = GetPath();
            if( !File.Exists(path)) return new Dolittle.Applications.Configuration.Topology(
                new Dictionary<Module, ModuleDefinition>(), 
                new Dictionary<Feature, FeatureDefinition>());
            
            var json = File.ReadAllText(path);
            var configuration = _serializer.FromJson<Dolittle.Applications.Configuration.Topology>(json, _serializationOptions);
            return configuration;
        }

        /// <inheritdoc/>
        public void Save(Dolittle.Applications.Configuration.Topology configuration)
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