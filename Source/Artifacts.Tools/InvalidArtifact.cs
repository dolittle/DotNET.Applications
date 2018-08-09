using System;

namespace Dolittle.Artifacts.Tools
{
    internal class InvalidArtifact : Exception
    {
        internal InvalidArtifact(Type type)
            : base($"Artifact {type.Name} with namespace = {type.Namespace} is invalid")
        { }
        internal InvalidArtifact(string typePath)
            : base($"Artifact with type path (a Module name + Feature names composition) {typePath} is invalid")
        { }
    }
}