using doLittle.Logging;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Logging.for_Logger.given
{
    public class all_dependencies
    {
        protected static Mock<ILogAppenders> appenders;

        Establish context = () => appenders = new Mock<ILogAppenders>();
    }
}
