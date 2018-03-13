/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Artifacts;

namespace Dolittle.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandArtifactType : IArtifactType
    {
        /// <inheritdoc/>
        public string Identifier => "Command";
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
                new CommandArtifactType()
            };
        }

        /// <inheritdoc/>
        IEnumerable<ArtifactTypeToType> ICanProvideArtifactTypeToTypeMaps.Provide()
        {
            return new []
            {
                new ArtifactTypeToType(new CommandArtifactType(), typeof(ICommand))
            };
        }
    }
}