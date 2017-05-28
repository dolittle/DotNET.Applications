using doLittle.Logging;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Logging.for_LogAppenders.given
{
    public class two_appenders : all_dependencies
    {
        protected static LogAppenders appenders;
        protected static Mock<ILogAppender> first_appender;
        protected static Mock<ILogAppender> second_appender;

        Establish context = () =>
        {
            appenders = new LogAppenders(log_appenders_configurators.Object);
            first_appender = new Mock<ILogAppender>();
            second_appender = new Mock<ILogAppender>();
            appenders.Add(first_appender.Object);
            appenders.Add(second_appender.Object);
        };
    }
}
