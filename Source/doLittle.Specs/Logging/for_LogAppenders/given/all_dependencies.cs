using System.Collections.Generic;
using doLittle.Execution;
using doLittle.Logging;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Logging.for_LogAppenders.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICanConfigureLogAppenders>> log_appenders_configurators;
        protected static List<ICanConfigureLogAppenders> actual_log_appenders_configurators;

        Establish context = () =>
        {
            log_appenders_configurators = new Mock<IInstancesOf<ICanConfigureLogAppenders>>();
            actual_log_appenders_configurators = new List<ICanConfigureLogAppenders>();
            log_appenders_configurators.Setup(l => l.GetEnumerator()).Returns(() => actual_log_appenders_configurators.GetEnumerator());
        };
    }
}
