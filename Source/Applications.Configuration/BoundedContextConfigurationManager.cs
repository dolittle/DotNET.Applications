/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Serialization.Json;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundedContextConfigurationManager"/>
    /// </summary>
    public class BoundedContextConfigurationManager : IBoundedContextConfigurationManager
    {
        const string _path   = "../bounded-context.json";
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="BoundedContextConfigurationManager"/>
        /// </summary>
        /// <param name="serializer"></param>
        public BoundedContextConfigurationManager(ISerializer serializer)
        {
            _serializer = serializer;
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
            var json = _serializer.ToJson(configuration);
            File.WriteAllText(path, json);
        }

        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(),_path);
        }
    }
}