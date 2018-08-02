using System;
using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Handling.for_CommandRequestToCommandConverter
{
    public class when_converting_with_properties
    {
        static TransactionCorrelationId correlation_id;
        static Mock<IArtifactTypeMap> artifact_type_map;
        static Artifact identifier;
        static CommandRequest request;
        static IDictionary<string, object> content;
        static CommandRequestToCommandConverter converter;
        static command_with_all_property_types result;
        const string a_string = "Fourty Two";
        const int an_integer = 42;
        const double a_double = 42.42;
        const float a_float = 42.42f;
        static Guid a_guid = Guid.NewGuid();
        static string AStringWithPascalCasing = "Fourty Two Pascals";
        static Guid a_concept_as_guid = Guid.NewGuid();
        static complex_type a_complex_type = new complex_type { 
            a_string = "Nested string",
            an_integer = 420,
            a_double = 420.420,
            a_float = 430.430f
        };
        static string[] an_enumerable_of_strings = new[] {"first", "second", "third"};
        static int[] an_enumerable_of_integers = new[] { 42,43,44 };
        static float[] an_enumerable_of_floats = new[] { 42.1f,43.2f,44.3f };
        static double[] an_enumerable_of_doubles = new[] { 42.4,43.5,44.6 };
        static Guid[] an_enumerable_of_guids = new[] { Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid() };

        Establish context = () => 
        {
            correlation_id = TransactionCorrelationId.New();
            identifier = Artifact.New();

            content = new Dictionary<string, object> {
                { "a_string", a_string},
                { "an_integer", an_integer },
                { "a_double", a_double },
                { "a_float", a_float },
                { "a_guid", a_guid },
                { "a_concept", a_concept_as_guid },
                { "a_complex_type", JObject.Parse(JsonConvert.SerializeObject(a_complex_type)) },
                { "astringwithpascalcasingontarget", AStringWithPascalCasing },
                { "AStringWithPascalCasingOnSource", AStringWithPascalCasing },
                { "an_enumerable_of_strings", an_enumerable_of_strings },
                { "an_enumerable_of_integers", an_enumerable_of_integers },
                { "an_enumerable_of_floats", an_enumerable_of_floats },
                { "an_enumerable_of_doubles", an_enumerable_of_doubles },
                { "an_enumerable_of_guids", an_enumerable_of_guids }
            };

            request = new CommandRequest(correlation_id, identifier, content);

            artifact_type_map = new Mock<IArtifactTypeMap>();
            artifact_type_map.Setup(_ => _.GetTypeFor(identifier)).Returns(typeof(command_with_all_property_types));

            converter = new CommandRequestToCommandConverter(artifact_type_map.Object);
        };

        Because of = () => result = converter.Convert(request) as command_with_all_property_types;

        It should_return_a_command = () => result.ShouldNotBeNull();
        It should_hold_a_string = () => result.a_string.ShouldEqual(a_string);
        It should_hold_an_integer = () => result.an_integer.ShouldEqual(an_integer);
        It should_hold_a_float = () => result.a_float.ShouldEqual(a_float);
        It should_hold_a_double = () => result.a_double.ShouldEqual(a_double);
        It should_hold_a_guid = () => result.a_guid.ShouldEqual(a_guid);
        It should_hold_a_complex_type_with_the_correct_string_value = () => result.a_complex_type.a_string.ShouldEqual(a_complex_type.a_string);
        It should_hold_a_complex_type_with_the_correct_integer_value = () => result.a_complex_type.an_integer.ShouldEqual(a_complex_type.an_integer);
        It should_hold_a_complex_type_with_the_correct_float_value = () => result.a_complex_type.a_float.ShouldEqual(a_complex_type.a_float);
        It should_hold_a_complex_type_with_the_correct_double_value = () => result.a_complex_type.a_double.ShouldEqual(a_complex_type.a_double);
        It should_hold_a_concept_converted_from_the_guid = () => result.a_concept.Value.ShouldEqual(a_concept_as_guid);
        It should_hold_a_string_with_pascal_casing_on_target_coming_in_with_lower_case = () => result.AStringWithPascalCasingOnTarget.ShouldEqual(AStringWithPascalCasing);
        It should_hold_a_string_with_pascal_casing_on_source_targetting_lower_case_on_target = () => result.astringwithpascalcasingonsource.ShouldEqual(AStringWithPascalCasing);
        It should_hold_an_enumerable_of_strings = () => result.an_enumerable_of_strings.ShouldEqual(an_enumerable_of_strings);
        It should_hold_an_enumerable_of_integers = () => result.an_enumerable_of_integers.ShouldEqual(an_enumerable_of_integers);
        It should_hold_an_enumerable_of_floats = () => result.an_enumerable_of_floats.ShouldEqual(an_enumerable_of_floats);
        It should_hold_an_enumerable_of_doubles = () => result.an_enumerable_of_doubles.ShouldEqual(an_enumerable_of_doubles);
        It should_hold_an_enumerable_of_guids = () => result.an_enumerable_of_guids.ShouldEqual(an_enumerable_of_guids);
    }
}