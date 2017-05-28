using System;
using doLittle.JSON.Serialization;
using Machine.Specifications;

namespace doLittle.JSON.Specs.Serialization.for_Serializer
{
    [Subject(typeof(Serializer))]
    public class when_serializing_a_concept : given.a_serializer
    {
        static ConceptAsGuid to_serialize;
        static string serialized_version;

        Establish context = () =>
                                {
                                    to_serialize = Guid.NewGuid();
                                };

        Because of = () => serialized_version = serializer.ToJson(to_serialize);

        It should_contain_the_guid_value = () => serialized_version.ShouldEqual("\"" + to_serialize.Value.ToString() + "\"");
    }
}