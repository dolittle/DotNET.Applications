// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="ICanHandleEvents">event handler</see> does not have the <see cref="EventHandlerAttribute"/>.
    /// </summary>
    public class MissingAttributeForEventHandler : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingAttributeForEventHandler"/> class.
        /// </summary>
        /// <param name="handlerType">Type of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="attributeType">The <see cref="Type" /> of the attribute that is missing.</param>
        /// <param name="attributeDefaultValueString">The default attribute default value string.</param>
        public MissingAttributeForEventHandler(Type handlerType, Type attributeType, string attributeDefaultValueString)
            : base($"Missing [{attributeType.Name.Replace("Attribute", string.Empty, StringComparison.InvariantCulture)}({attributeDefaultValueString})] attribute on '{handlerType.AssemblyQualifiedName}'.")
        {
        }
    }
}