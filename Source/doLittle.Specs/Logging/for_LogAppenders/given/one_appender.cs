using doLittle.Logging;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Logging.for_LogAppenders.given
{
    public class one_appender : all_dependencies
    {
        protected static LogAppenders appenders;
        protected static Mock<ILogAppender> appender;

        Establish context = () =>
        {
            appenders = new LogAppenders(log_appenders_configurators.Object);
            appender = new Mock<ILogAppender>();
            appenders.Add(appender.Object);
        };
    }
}
