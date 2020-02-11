// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideStreamFilters"/> for <see cref="ICanHandleEvents"/> implementations.
    /// </summary>
    public class EventHandlerFilterProvider : ICanProvideStreamFilters
    {
        readonly IImplementationsOf<ICanHandleEvents> _handlerTypes;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerFilterProvider"/> class.
        /// </summary>
        /// <param name="handlerTypes">All the <see cref="ICanHandleEvents"/> implementations.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlerFilterProvider(
            IImplementationsOf<ICanHandleEvents> handlerTypes,
            ILogger logger)
        {
            _handlerTypes = handlerTypes;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<ICanFilterEventsInStream> Provide()
        {
            return _handlerTypes.Select(handlerType =>
            {
                var eventHandlerAttribute = handlerType.GetCustomAttribute<EventHandlerAttribute>();
                _logger.Information($"Providing '{handlerType.AssemblyQualifiedName}' with identifier '{eventHandlerAttribute.Id}'");
                return new EventHandlerFilterAndProcessor(eventHandlerAttribute.Id);
            });
        }
    }
}