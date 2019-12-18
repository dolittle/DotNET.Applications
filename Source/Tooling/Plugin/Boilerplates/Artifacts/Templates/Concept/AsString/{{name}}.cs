using Dolittle.Concepts;

namespace {{namespace}}
{
    public class {{name}} : ConceptAs<string>
    {
        public static implicit operator {{name}}(string value)
        {
            return new {{name}} {Value = value};
        }
    }
}
