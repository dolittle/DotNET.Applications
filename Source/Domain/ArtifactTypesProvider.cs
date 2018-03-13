/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Artifacts;

namespace Dolittle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class AggregateRootArtifactType : IArtifactType
    {
        /// <inheritdoc/>
        public string Identifier => "AggregateRoot";
    }

    


    /// <summary>
    /// 
    /// </summary>
    public class ArtifactTypesProvider : ICanProvideArtifactTypes, ICanProvideArtifactTypeToTypeMaps
    {
        /// <inheritdoc/>
        public IEnumerable<IArtifactType> Provide()
        {
            return new IArtifactType[] { 
                new AggregateRootArtifactType()
            };
        }

        /// <inheritdoc/>
        IEnumerable<ArtifactTypeToType> ICanProvideArtifactTypeToTypeMaps.Provide()
        {
            return new []
            {
                new ArtifactTypeToType(new AggregateRootArtifactType(), typeof(AggregateRoot))
            };
        }
    }
}