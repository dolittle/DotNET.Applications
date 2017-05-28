using System;
using doLittle.Mapping;
using Machine.Specifications;

namespace doLittle.Specs.Mapping.for_Maps
{
    public class when_asking_for_map_for_unknown_combination : given.no_maps
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => maps.GetFor(typeof(Source), typeof(Target)));

        It should_throw_missing_map_exception = () => exception.ShouldBeOfExactType<MissingMapException>();
    }
}
