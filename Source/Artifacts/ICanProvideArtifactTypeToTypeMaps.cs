/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Defines a system that is capable of providing <see cref="ArtifactTypeToType"/> maps
    /// </summary>
    public interface ICanProvideArtifactTypeToTypeMaps
    {
        /// <summary>
        /// Provide a collection of <see cref="ArtifactTypeToType"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{ArtifactTypeToType}"/></returns>
        IEnumerable<ArtifactTypeToType> Provide();
    }
}