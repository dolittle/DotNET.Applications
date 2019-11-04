using Dolittle.Concepts;

namespace {{namespace}}
{
    public class {{name}} : ConceptAs<int>
    {
        public static implicit operator {{name}}(int value)
        {
            return new {{name}} {Value = value};
        }
    }
}
