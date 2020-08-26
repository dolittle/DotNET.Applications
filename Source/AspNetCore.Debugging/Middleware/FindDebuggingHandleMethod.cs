// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.AspNetCore.Debugging.Handlers;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// An implementation of <see cref="IFindDebuggingHandleMethod"/>.
    /// </summary>
    public class FindDebuggingHandleMethod : IFindDebuggingHandleMethod
    {
        /// <inheritdoc/>
        public MethodInfo FindMethod(IDebuggingHandler handler, Type handlerInterface, Type artifactType)
        {
            var handlerInterfaceTypeInfo = handlerInterface.GetTypeInfo();
            var implementedGenericInterfaces = handler.GetType().GetInterfaces().Where(_ => _.IsGenericType);
            var matchingInterfaces = implementedGenericInterfaces.Where(_ => _.GetGenericTypeDefinition().GetTypeInfo() == handlerInterfaceTypeInfo);
            var suitableInterfaces = matchingInterfaces.Where(_ => _.GenericTypeArguments.Length == 1 && _.GenericTypeArguments[0].IsAssignableFrom(artifactType));

            var interfaceMethods = suitableInterfaces.SelectMany(_ => _.GetMethods());
            var handleMethods = interfaceMethods.Where(_ => _.Name.StartsWith("Handle", StringComparison.InvariantCultureIgnoreCase));

            ThrowIfNoHandleMethodsWasFound(handleMethods, handler, handlerInterface, artifactType);
            ThrowIfMoreThanOneHandleMethodsWasFound(handleMethods, handler, handlerInterface, artifactType);

            return handleMethods.First();
        }

        void ThrowIfNoHandleMethodsWasFound(IEnumerable<MethodInfo> handleMethods, IDebuggingHandler handler, Type handlerInterface, Type artifactType)
        {
            if (!handleMethods.Any())
            {
                throw new NoAppropriateHandleMethodFound(handler, handlerInterface, artifactType);
            }
        }

        void ThrowIfMoreThanOneHandleMethodsWasFound(IEnumerable<MethodInfo> handleMethods, IDebuggingHandler handler, Type handlerInterface, Type artifactType)
        {
            if (handleMethods.Count() > 1)
            {
                throw new MoreThanOneAppropriateHandleMethodFound(handler, handlerInterface, artifactType);
            }
        }
    }
}