/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Strings;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines the mapping system between <see cref="Type"/> and an <see cref="IApplicationStructure"/>
    /// </summary>
    public interface IApplicationStructureMap
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStringFormat>  Formats { get; }

        /// <summary>
        /// Check if a specific type fits in the <see cref="IApplicationStructure"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        /// <returns>True if it fits, false if not</returns>
        bool DoesFitInStructure(Type type);

        /// <summary>
        /// Check if any of the given types fits in the <see cref="IApplicationStructure"/>
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        bool DoesAnyFitInStructure(IEnumerable<Type> types);

        /// <summary>
        /// Get the mest matching type for a collection of types
        /// </summary>
        /// <param name="types"><see cref="IEnumerable{Type}">Types</see> to get for</param>
        /// <returns><see cref="Type"/> that matches best</returns>
        Type GetBestMatchingTypeFor(IEnumerable<Type> types);
    }
}