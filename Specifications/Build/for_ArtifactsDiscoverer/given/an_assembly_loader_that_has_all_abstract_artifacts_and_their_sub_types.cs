using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications;
using Moq;

using Specs;

namespace Dolittle.Build.Specs.for_ArtifactsDiscoverer.given
{
    public class an_assembly_loader_that_has_all_abstract_artifacts_and_their_sub_types : given.all
    {
        protected static readonly IEnumerable<Type> abstract_types = new []
        {
            typeof(AbstractCommand),
            typeof(AbstractEvent),
            typeof(AbstractEventSource),
            typeof(AbstractQuery),
            typeof(AbstractReadModel),
        };
        protected static readonly IEnumerable<Type> non_abstract_subtypes_of_the_abstract_types = new []
        {
            typeof(ImplementationOfAbstractCommand),
            typeof(ImplementationOfAbstractEvent),
            typeof(ImplementationOfAbstractEventSource),
            typeof(ImplementationOfAbstractQuery),
            typeof(ImplementationOfAbstractReadModel),
        };
        protected static Mock<AssemblyLoader> assembly_loader_mock;

        Establish context = () =>
        {
            assembly_loader_mock = new Mock<AssemblyLoader>();

            var assembly_mock = new Mock<Assembly>();
            assembly_mock.Setup(_ => _.ExportedTypes).Returns(abstract_types.Concat(non_abstract_subtypes_of_the_abstract_types));
            assembly_loader_mock.Setup(_ => _.GetProjectReferencedAssemblies()).Returns(new []{assembly_mock.Object});
        };
    }
}