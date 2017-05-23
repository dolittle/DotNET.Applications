/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;
using Bifrost.Applications;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandRequestConverter"/>
    /// </summary>
    public class CommandRequestConverter : ICommandRequestConverter
    {
        IApplicationResourceResolver _applicationResourceResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationResourceResolver"></param>
        public CommandRequestConverter(IApplicationResourceResolver applicationResourceResolver)
        {
            _applicationResourceResolver = applicationResourceResolver;
        }

        /// <inheritdoc/>
        public ICommand Convert(CommandRequest request)
        {
            // todo: Cache it per transaction / command context 

            var type = _applicationResourceResolver.Resolve(request.Type);

            // todo: Verify that it is a an ICommand
            var instance = Activator.CreateInstance(type) as ICommand;

            var properties = type.GetTypeInfo().DeclaredProperties.ToDictionary(p => p.Name, p => p);
            // todo: Verify that the command shape matches 100% - do not allow anything else

            // todo: Convert to target type if mismatch
            request.Content.Keys.ForEach(property => properties[property]?.SetValue(instance, request.Content[property]));

            return instance;
        }
    }
}