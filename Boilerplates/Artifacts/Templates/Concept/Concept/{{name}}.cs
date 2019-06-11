using Dolittle.Concepts;

namespace {{namespace}}
{
    public class {{name}} : ConceptAs<{{conceptType}}>
    {
        public static implicit operator {{name}}({{conceptType}} value)
        {
            return new {{name}} {Value = value};
        }
    }
}
