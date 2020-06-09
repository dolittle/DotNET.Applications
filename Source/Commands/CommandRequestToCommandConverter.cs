// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json.Linq;

namespace Dolittle.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandRequestToCommandConverter"/>.
    /// </summary>
    [Singleton]
    public class CommandRequestToCommandConverter : ICommandRequestToCommandConverter
    {
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ConcurrentDictionary<Artifact, Type> _commandTypes = new ConcurrentDictionary<Artifact, Type>();
        readonly ISerializer _serializer;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRequestToCommandConverter"/> class.
        /// </summary>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping between types and artifacts.</param>
        /// <param name="serializer"><see cref="ISerializer"/> for serialization.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public CommandRequestToCommandConverter(IArtifactTypeMap artifactTypeMap, ISerializer serializer, ILogger logger)
        {
            _artifactTypeMap = artifactTypeMap;
            _serializer = serializer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public ICommand Convert(CommandRequest request)
        {
            try
            {
                _logger.Trace("Converting command request for command {Command}", request.Type);
                var instance = GetCommandFrom(request.Type);

                // todo: Verify that the command shape matches 100% - do not allow anything else
#pragma warning disable CA1308
                var properties = instance.GetType().GetProperties().ToDictionary(p => p.Name.ToLowerInvariant(), p => p);
#pragma warning restore CA1308

                CopyPropertiesFromRequestToCommand(request, instance, properties);
                return instance;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "Error ocurred while converting command request for command {CommandType}", request.Type);
                throw;
            }
        }

        ICommand GetCommandFrom(Artifact artifact)
        {
            try
            {
                var type = _commandTypes.GetOrAdd(artifact, (artifact) => _artifactTypeMap.GetTypeFor(artifact));
                return Activator.CreateInstance(type) as ICommand;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "Could not convert command request. Artifact {Artifact} is not a valid {ICommand} type", artifact, typeof(ICommand));
                throw new ArtifactIsNotCommand(artifact);
            }
        }

        void CopyPropertiesFromRequestToCommand(CommandRequest request, ICommand instance, Dictionary<string, PropertyInfo> properties)
        {
            request.Content.Keys.ForEach(propertyName =>
            {
#pragma warning disable CA1308
                var lowerCasedPropertyName = propertyName.ToLowerInvariant();
#pragma warning restore CA1308
                if (properties.ContainsKey(lowerCasedPropertyName))
                {
                    var property = properties[lowerCasedPropertyName];
                    object value = request.Content[propertyName];

                    value = HandleValue(property.PropertyType, value);
                    property.SetValue(instance, value);
                }
            });
        }

        object HandleValue(Type targetType, object value)
        {
            if (value is JArray || value is JObject)
            {
                value = _serializer.FromJson(targetType, value.ToString());
            }
            else if (targetType.IsConcept())
            {
                value = ConceptFactory.CreateConceptInstance(targetType, value);
            }
            else if (targetType == typeof(DateTimeOffset) && value.GetType() == typeof(DateTime))
            {
                value = new DateTimeOffset((DateTime)value);
            }
            else if (targetType.IsEnum)
            {
                value = Enum.Parse(targetType, value.ToString());
            }
            else if (targetType == typeof(Guid))
            {
                value = Guid.Parse(value.ToString());
            }
            else
            {
                if (!targetType.IsAssignableFrom(value.GetType()))
                {
                    value = System.Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
                }
            }

            return value;
        }
    }
}
