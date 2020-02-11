// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Exception that gets thrown when a type is expected to implement <see cref="ICanHandleEvents"/> but doesn't.
    /// </summary>
    public class TypeIsNotAnEventHandler : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeIsNotAnEventHandler"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that does not implement <see cref="ICanHandleEvents"/>.</param>
        public TypeIsNotAnEventHandler(Type type)
            : base($"Type '{type.AssemblyQualifiedName}' does not implement '{typeof(ICanHandleEvents).FullName}'")
        {
        }
    }
}