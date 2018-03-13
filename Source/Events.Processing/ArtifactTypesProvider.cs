/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.Events.Processing;

namespace Dolittle.Events
{

    /// <summary>
    /// 
    /// </summary>
    public class EventProcessorArtifactType : IArtifactType
    {
        /// <inheritdoc/>
        public string Identifier => "EventProcessor";
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
                new EventArtifactType(),
                new EventProcessorArtifactType()
            };
        }

        /// <inheritdoc/>
        IEnumerable<ArtifactTypeToType> ICanProvideArtifactTypeToTypeMaps.Provide()
        {
            return new []
            {
                new ArtifactTypeToType(new EventArtifactType(), typeof(IEvent)),
                new ArtifactTypeToType(new EventProcessorArtifactType(), typeof(ICanProcessEvents))
            };
        }
    }
}