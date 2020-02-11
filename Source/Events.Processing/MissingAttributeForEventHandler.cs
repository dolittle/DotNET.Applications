// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Processing
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
        public MissingAttributeForEventHandler(Type handlerType)
            : base($"Missing [EventHandler(\"00000000-0000-0000-0000-000000000000\")] attribute on '{handlerType.AssemblyQualifiedName}'. Any implementations of '{typeof(ICanHandleEvents).FullName}' needs this attribute")
        {
        }
    }
}