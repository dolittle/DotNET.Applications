// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.Heads
{
    /// <summary>
    /// Defines a system that defines runtime application services.
    /// </summary>
    public interface IDefineRuntimeServices
    {
        /// <summary>
        /// Gets the <see cref="RuntimeServiceDefinition"/> for all exposed services.
        /// </summary>
        IEnumerable<RuntimeServiceDefinition> Services { get; }
    }
}