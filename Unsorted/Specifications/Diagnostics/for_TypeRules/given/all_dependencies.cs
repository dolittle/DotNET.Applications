using Dolittle.DependencyInversion;
using Dolittle.Diagnostics;
using Dolittle.Execution;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Specs.Diagnostics.for_TypeRules.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IContainer> container;
        protected static Mock<IProblemsFactory> problems_factory_mock;
        protected static Mock<IProblemsReporter> problems_reporter_mock;

        Establish context = () =>
        {
            type_finder = new Mock<ITypeFinder>();
            container = new Mock<IContainer>();
            problems_factory_mock = new Mock<IProblemsFactory>();
            problems_reporter_mock = new Mock<IProblemsReporter>();
        };
    }
}
