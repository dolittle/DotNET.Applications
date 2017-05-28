using System;
using doLittle.Strings;
using Machine.Specifications;

namespace doLittle.Specs.Strings.for_StringFormat
{
    public class when_creating_without_separators
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => new StringFormat(new ISegment[] { new NullSegment() }));

        It should_throw_missing_separator = () => exception.ShouldBeOfExactType<MissingSeparators>();
    }
}
