using System;
using doLittle.Execution;
using Machine.Specifications;

namespace doLittle.Specs.Execution.for_WriteOnceExpandoObject
{
    [Behaviors]
    public class a_read_only_container
    {
        protected static Exception exception;
        It should_throw_a_read_only_object_exception = () => exception.ShouldBeOfExactType<ReadOnlyObjectException>();
    }
}
