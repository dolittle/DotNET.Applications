using System;
using Bifrost.Concepts;

namespace Bifrost.Specs.Extensions.for_StringExtensions
{
    public class ConceptAsGuid : ConceptAs<Guid>
    {
        public static implicit operator ConceptAsGuid(Guid guid)
        {
            return new ConceptAsGuid() { Value = guid };
        }
    }
}