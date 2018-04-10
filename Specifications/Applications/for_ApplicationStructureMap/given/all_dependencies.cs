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

        protected static IDictionary<ApplicationArea, IEnumerable<IStringFormat>> formats_per_area;

        Establish context = () => 
        {
            application = new Mock<IApplication>();
            formats_per_area = new Dictionary<ApplicationArea, IEnumerable<IStringFormat>>();  
        };      
    }
}