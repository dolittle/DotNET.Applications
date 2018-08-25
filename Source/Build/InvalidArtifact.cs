/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Build
{
    internal class InvalidArtifact : Exception
    {
        internal InvalidArtifact() 
            : base("Invalid Artifacts was discovered") 
        { }
        internal InvalidArtifact(Type type)
            : base($"Artifact {type.Name} with namespace = {type.Namespace} is invalid")
        { }
        internal InvalidArtifact(string typePath)
            : base($"Artifact with type path (a Module name + Feature names composition) {typePath} is invalid")
        { }
    }
}