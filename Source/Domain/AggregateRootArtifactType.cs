/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Artifacts;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents the identifier and map to the concrete <see cref="IAggregateRoot"/> type
    /// </summary>
    public class AggregateRootArtifactType : IArtifactTypeMapFor<IAggregateRoot>
    {
        /// <inheritdoc/>
        public string Identifier => "AggregateRoot";
    }
}