/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dolittle.Build.Artifact
{
    /// <summary>
    /// Represents a class that basically handles the interations with a <see cref="ArtifactsConfiguration"/>
    /// </summary>
    public class ArtifactsConfigurationHandler
    {
        readonly ArtifactsConfigurationManager _configurationManager;
        readonly IEnumerable<ArtifactType> _artifactTypes;
        readonly ILogger _logger;

        readonly static ISerializationOptions _serializationOptions = SerializationOptions
            .Custom(SerializationOptionsFlags.None, 
            new JsonConverter[] {},
            serializer => {
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serializer.Formatting = Formatting.Indented;
                serializer.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
            });
        
        /// <summary>
        /// Initializes an instance of <see cref="ArtifactsConfigurationHandler"/>
        /// </summary>
        /// <param name="configurationSerializer">The serializer for the <see cref="ArtifactsConfigurationManager"/></param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        /// <param name="logger"></param>
        public ArtifactsConfigurationHandler(ISerializer configurationSerializer, IEnumerable<ArtifactType> artifactTypes, ILogger logger)
        {
            _configurationManager = new ArtifactsConfigurationManager(configurationSerializer, _serializationOptions, logger);
            _artifactTypes = artifactTypes;
            _logger = logger;
        }

        /// <summary>
        /// Loads the <see cref="ArtifactsConfiguration"/> from file and uses it to build the <see cref="ArtifactsConfiguration"/> using the <see cref="ArtifactsConfigurationBuilder"/>
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies</param>
        /// <param name="boundedContextConfiguration">The <see cref="BoundedContextConfiguration"/> that's used for building the <see cref="ArtifactsConfiguration"/></param>
        public ArtifactsConfiguration Build(Type[] types, BoundedContextConfiguration boundedContextConfiguration)
        {
            var artifactsConfiguration = _configurationManager.Load();
            return new ArtifactsConfigurationBuilder(types, artifactsConfiguration, _artifactTypes, _logger).Build(boundedContextConfiguration);
        }
        internal void Save(ArtifactsConfiguration config)
        {
            _configurationManager.Save(config);
        }
    }
}