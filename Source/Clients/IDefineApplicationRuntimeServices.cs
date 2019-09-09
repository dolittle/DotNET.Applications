/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Clients
{
    /// <summary>
    /// Defines a system that defines runtime application services
    /// </summary>
    public interface IDefineApplicationRuntimeServices
    {
        /// <summary>
        /// Gets the <see cref="RuntimeServiceDefinition"/> for all exposed services
        /// </summary>
        IEnumerable<RuntimeServiceDefinition>   Services { get; }
    }
}