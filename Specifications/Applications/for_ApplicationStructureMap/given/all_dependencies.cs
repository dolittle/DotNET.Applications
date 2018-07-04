using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Strings;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationStructureMap.given
{
    public class all_dependencies
    {
        protected static Mock<IApplication> application;

        protected static IEnumerable<IStringFormat> formats;

        Establish context = () => 
        {
            application = new Mock<IApplication>();
            formats = new IStringFormat[0];
        };      
    }
}