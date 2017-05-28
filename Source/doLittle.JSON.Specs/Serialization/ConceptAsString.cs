using System;
using doLittle.Concepts;

namespace doLittle.JSON.Specs.Serialization
{
    public class ConceptAsString : ConceptAs<String>
    {
        public static implicit operator ConceptAsString(string value)
        {
            return new ConceptAsString { Value = value };
        }
    }
}