// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Reflection;

namespace Dolittle.Build
{
    /// <summary>
    /// Exception that gets thrown when an event processor has an empty Id.
    /// </summary>
    public class EventProcessorWithEmptyIdNotAllowed : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorWithEmptyIdNotAllowed"/> class.
        /// </summary>
        /// <param name="method"><see cref="MethodInfo"/> that represents the event processor.</param>
        public EventProcessorWithEmptyIdNotAllowed(MethodInfo method)
            : base($"Method with signature '{method.Name}({string.Join(", ", method.GetParameters().Select(_ => _.ParameterType.Name))})' does not have a unique identifier representing the event processor.")
        {
        }
    }
}