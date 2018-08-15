/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Artifacts.Tools
{
    internal class DuplicateArtifact : Exception
    {
        public DuplicateArtifact() : base("Duplicate artifacts was found. Are you missing a migrator? ")
        {
        }

        public DuplicateArtifact(string message) : base(message)
        {
        }

    }
}