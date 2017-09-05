using System;
using doLittle.Concepts;

namespace doLittle.Specs.Concepts.for_StringExtensions
{
    public class ConceptAsGuid : ConceptAs<Guid>
    {
        public static implicit operator ConceptAsGuid(Guid guid)
        {
            return new ConceptAsGuid() { Value = guid };
        }
    }
}