// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Artifacts;

namespace Dolittle.Commands
{
    /// <summary>
    /// Exception that gets thrown when an artifact was expected to be a command, but was not.
    /// </summary>
    public class ArtifactIsNotCommand : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactIsNotCommand"/> class.
        /// </summary>
        /// <param name="artifact">The <see cref="Artifact" />.</param>
        public ArtifactIsNotCommand(Artifact artifact)
            : base($"The artifact {artifact} is not a command")
        {
        }
    }
}
