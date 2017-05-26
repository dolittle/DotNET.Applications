using System;
using Bifrost.Concepts;

namespace Bifrost.JSON.Specs.Serialization
{
    public class ConceptAsString : ConceptAs<String>
    {
        public static implicit operator ConceptAsString(string value)
        {
            return new ConceptAsString { Value = value };
        }
    }
}