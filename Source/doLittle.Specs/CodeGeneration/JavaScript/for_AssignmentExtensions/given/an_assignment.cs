using doLittle.CodeGeneration.JavaScript;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.CodeGeneration.JavaScript.for_AssignmentExtensions.given
{
    public class an_assignment
    {
        protected static MyAssignment assignment;

        Establish context = () => assignment = new MyAssignment();

    }
}
