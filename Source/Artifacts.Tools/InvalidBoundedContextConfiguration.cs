using System;

namespace Dolittle.Artifacts.Tools
{
    internal class InvalidBoundedContextConfiguration : Exception
    {
        internal InvalidBoundedContextConfiguration(string message)
            : base(message) {}   
    }
}