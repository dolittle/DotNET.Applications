// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.Heads
{
    /// <summary>
    /// Defines a system for gathering all <see cref="RuntimeServiceDefinition"/>.
    /// </summary>
    public interface IRuntimeServices
    {
        /// <summary>
        /// Gets the <see cref="RuntimeServiceDefinition"/> for all exposed services.
        /// </summary>
        IEnumerable<RuntimeServiceDefinition> Services { get; }
    }
}