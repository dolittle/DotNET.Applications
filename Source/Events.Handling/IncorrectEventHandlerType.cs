// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when a type is expected to implement type that can handle events, but doesn't.
    /// </summary>
    public class IncorrectEventHandlerType : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncorrectEventHandlerType"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that does not implement the correct event handler type.</param>
        /// <param name="expectedType">The event handler <see cref="Type" /> that was expected.</param>
        public IncorrectEventHandlerType(Type type, Type expectedType)
            : base($"Type '{type.AssemblyQualifiedName}' does not implement event handler type '{expectedType.FullName}'.")
        {
        }
    }
}