/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Logging;
using Dolittle.Strings;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a system for resolving <see cref="IApplicationLocation"/>
    /// </summary>
    public interface IApplicationLocationResolver
    {
        /// <summary>
        /// Check if one can resolve from given <see cref="Type"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check if can be resolved</param>
        /// <returns>True if it can be resolved, false if not</returns>
        bool CanResolve(Type type);

        /// <summary>
        /// Resolve a <see cref="Type">type's</see> <see cref="IApplicationLocation">location</see>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to resolve</param>
        /// <returns><see cref="IApplicationLocation">Location</see> within the <see cref="IApplication"/></returns>
        IApplicationLocation Resolve(Type type);
    }
}