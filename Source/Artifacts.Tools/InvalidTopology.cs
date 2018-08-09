using System;

namespace Dolittle.Artifacts.Tools
{
    internal class InvalidTopology : Exception
    {
        internal InvalidTopology(string message)
            : base(message) {}
    }
}