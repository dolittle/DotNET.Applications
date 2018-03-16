using System;
using Dolittle.Concepts;

namespace Dolittle.FluentValidation
{
    public class ConceptAsString : ConceptAs<String>
    {
        public static implicit operator ConceptAsString(string value)
        {
            return new ConceptAsString { Value = value };
        }
    }
}