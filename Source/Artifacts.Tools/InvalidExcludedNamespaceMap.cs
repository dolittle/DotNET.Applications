using System;
using System.Runtime.Serialization;

namespace Dolittle.Artifacts.Tools
{
    internal class InvalidExcludedNamespaceMap : Exception
    {

        public InvalidExcludedNamespaceMap(string message) : base(message)
        {
        }
    }
}