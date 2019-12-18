using Dolittle.Concepts;
using System;

namespace {{namespace}}
{
    public class {{name}} : ConceptAs<Guid>
    {
        public static implicit operator {{name}}(Guid value)
        {
            return new {{name}} {Value = value};
        }
    }
}
