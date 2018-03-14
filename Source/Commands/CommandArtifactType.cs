/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Artifacts;

namespace Dolittle.Commands
{
    /// <summary>
    /// Represents the identifier and map to the concrete <see cref="ICommand"/> type
    /// </summary>
    public class CommandArtifactType : IArtifactTypeMapFor<ICommand>
    {
        /// <inheritdoc/>
        public string Identifier => "Command";
    }
}