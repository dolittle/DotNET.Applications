// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Microservice.Configuration;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IMicroserviceLoader"/>.
    /// </summary>
    [Singleton]
    public class MicroserviceLoader : IMicroserviceLoader
    {
        readonly ISerializer _serializer;
        readonly ILogger _logger;

        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(
            callback: serializer =>
             {
                 serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
                 serializer.Formatting = Formatting.Indented;
             });

        MicroserviceConfiguration _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MicroserviceLoader"/> class.
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for working with configuration as JSON.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public MicroserviceLoader(ISerializer serializer, ILogger logger)
        {
            _serializer = serializer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public MicroserviceConfiguration Load()
        {
            return Load(Path.Combine("..", "microservice.json"));
        }

        /// <inheritdoc/>
        public MicroserviceConfiguration Load(string relativePath)
        {
            if (_instance != null) return _instance;

            var path = GetPath(relativePath);
            if (!File.Exists(path))
            {
                _logger.Error("Bounded context configuration expected at path {Path} was not found", path);
                throw new MissingMicroserviceConfiguration(path);
            }

            var json = File.ReadAllText(path);
            var configuration = _serializer.FromJson<MicroserviceConfiguration>(json, _serializationOptions);
            _instance = configuration;
            return _instance;
        }

        string GetPath(string relativePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), relativePath);
        }
    }
}