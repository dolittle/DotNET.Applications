// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.SDK.CodeAnalysis
{
    /// <summary>
    /// Exception that gets thrown when a type is not an interface.
    /// </summary>
    public class TypeMustBeAnInterface : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMustBeAnInterface"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that is not an interface.</param>
        public TypeMustBeAnInterface(Type type)
            : base($"Type '{type.FullName}' must be an interface")
        {
        }
    }
}