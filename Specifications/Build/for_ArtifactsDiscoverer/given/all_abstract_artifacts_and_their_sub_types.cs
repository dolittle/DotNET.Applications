// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications;
using Moq;

using Specs.Feature;
using IAssemblyContext = Dolittle.Assemblies.IAssemblyContext;

namespace Dolittle.Build.for_ArtifactsDiscoverer.given
{
    public class all_abstract_artifacts_and_their_sub_types : all
    {
        protected static readonly IEnumerable<Type> abstract_types = new[]
        {
            typeof(AbstractCommand),
            typeof(AbstractEvent),
            typeof(AbstractEventSource),
            typeof(AbstractQuery),
            typeof(AbstractReadModel),
        };

        protected static readonly IEnumerable<Type> non_abstract_subtypes_of_the_abstract_types = new[]
        {
            typeof(ImplementationOfAbstractCommand),
            typeof(ImplementationOfAbstractEvent),
            typeof(ImplementationOfAbstractEventSource),
            typeof(ImplementationOfAbstractQuery),
            typeof(ImplementationOfAbstractReadModel),
        };

        protected static Mock<IAssemblyContext> assembly_context;

        Establish context = () =>
        {
            assembly_context = new Mock<IAssemblyContext>();

            var assembly = new Mock<Assembly>();
            assembly.Setup(_ => _.ExportedTypes).Returns(abstract_types.Concat(non_abstract_subtypes_of_the_abstract_types));
            assembly_context.Setup(_ => _.GetProjectReferencedAssemblies()).Returns(new[] { assembly.Object });
        };
    }
}