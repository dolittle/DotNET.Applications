using doLittle.Diagnostics;
using Machine.Specifications;

namespace doLittle.Specs.Diagnostics.for_ProblemsFactory
{
    public class when_creating 
    {
        static ProblemsFactory factory;
        static IProblems result;

        Establish context = () => factory = new ProblemsFactory();

        Because of = () => result = factory.Create();

        It should_return_an_instance = () => result.ShouldNotBeNull();
    }
}
