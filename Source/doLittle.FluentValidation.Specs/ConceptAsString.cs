using System;
using Bifrost.Concepts;

namespace Bifrost.FluentValidation.Specs
{
    public class ConceptAsString : ConceptAs<String>
    {
        public static implicit operator ConceptAsString(string value)
        {
            return new ConceptAsString { Value = value };
        }
    }
}