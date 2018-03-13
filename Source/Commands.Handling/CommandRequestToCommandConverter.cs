/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Collections;
using Dolittle.Applications;
using Dolittle.Runtime.Commands;

namespace Dolittle.Commands.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandRequestToCommandConverter"/>
    /// </summary>
    public class CommandRequestToCommandConverter : ICommandRequestToCommandConverter
    {
        IApplicationArtifactResolver _applicationArtifactResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationArtifactResolver"></param>
        public CommandRequestToCommandConverter(IApplicationArtifactResolver applicationArtifactResolver)
        {
            _applicationArtifactResolver = applicationArtifactResolver;
        }

        /// <inheritdoc/>
        public ICommand Convert(CommandRequest request)
        {
            // todo: Cache it per transaction / command context 

            var type = _applicationArtifactResolver.Resolve(request.Type);

            // todo: Verify that it is a an ICommand
            var instance = Activator.CreateInstance(type) as ICommand;

            var properties = type.GetTypeInfo().DeclaredProperties.ToDictionary(p => p.Name, p => p);
            // todo: Verify that the command shape matches 100% - do not allow anything else

            // todo: Convert to target type if mismatch
            request.Content.Keys.ForEach(propertyName =>
            {
                if (properties.ContainsKey(propertyName))
                {
                    var property = properties[propertyName];
                    object value = request.Content[propertyName];
                    if (property.PropertyType.IsConcept())
                    {
                        value = ConceptFactory.CreateConceptInstance(property.PropertyType, value);
                    }
                    else if (property.PropertyType == typeof(DateTimeOffset) && value.GetType() == typeof(DateTime))
                    {
                        value = new DateTimeOffset((DateTime)value);
                    }

                    property.SetValue(instance, value);
                }
            });

            return instance;
        }
    }
}