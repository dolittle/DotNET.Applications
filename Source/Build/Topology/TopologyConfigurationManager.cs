// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents an implementation of <see cref="ITopologyConfigurationManager"/>.
    /// </summary>
    [Singleton]
    public class TopologyConfigurationManager : ITopologyConfigurationManager
    {
        readonly ISerializer _serializer;

        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
            serializer =>
            {
                serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
                serializer.Formatting = Formatting.Indented;
            });

        readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopologyConfigurationManager"/> class.
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for working with configuration as JSON.</param>
        /// <param name="buildTaskConfiguration">Current <see cref="BuildTaskConfiguration"/>.</param>
        public TopologyConfigurationManager(
            ISerializer serializer,
            BuildTaskConfiguration buildTaskConfiguration)
        {
            _serializer = serializer;
            _path = Path.Combine(buildTaskConfiguration.DolittleFolder, "topology.json");
        }

        /// <inheritdoc/>
        public Applications.Configuration.Topology Load()
        {
            var path = GetPath();
            if (!File.Exists(path))
            {
                return new Applications.Configuration.Topology(
                            new Dictionary<Module, ModuleDefinition>(),
                            new Dictionary<Feature, FeatureDefinition>());
            }

            var json = File.ReadAllText(path);
            return _serializer.FromJson<Applications.Configuration.Topology>(json, _serializationOptions);
        }

        /// <inheritdoc/>
        public void Save(Applications.Configuration.Topology configuration)
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