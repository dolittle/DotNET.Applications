using System;
using doLittle.Concepts;
using doLittle.Specs.Concepts.given;
using Machine.Specifications;

namespace doLittle.Specs.Concepts.for_ConceptMap
{
    [Subject(typeof(ConceptMap))]
    public class when_getting_the_primitive_type_from_a_string_concept
    {
        static Type result;

        Because of = () => result = ConceptMap.GetConceptValueType(typeof (concepts.StringConcept));

        It should_get_a_string = () => result.ShouldEqual(typeof(string));
    }
}