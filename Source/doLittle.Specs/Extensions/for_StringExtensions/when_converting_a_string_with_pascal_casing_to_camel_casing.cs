using doLittle.Extensions;
using Machine.Specifications;

namespace doLittle.Specs.Extensions.for_StringExtensions
{
    public class when_converting_a_string_with_pascal_casing_to_camel_casing
    {
        static string result;

        Because of = () => result = "PascalCased".ToCamelCase();

        It should_turn_it_into_camel_case = () => result.ShouldEqual("pascalCased");
    }
}
