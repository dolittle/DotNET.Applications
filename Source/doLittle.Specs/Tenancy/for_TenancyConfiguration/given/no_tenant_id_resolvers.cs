using System;
using System.Collections.Generic;
using doLittle.DependencyInversion;
using doLittle.Execution;
using doLittle.Tenancy;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Tenancy.for_TenancyConfiguration.given
{
    public class no_tenant_id_resolvers
    {
        protected static Mock<IContainer> container;
        protected static Mock<ITypeFinder> type_finder;
        protected static TenancyConfiguration configuration;
        protected static List<Type> resolvers;

        Establish context = () =>
        {
            resolvers = new List<Type>();
            type_finder = new Mock<ITypeFinder>();
            type_finder.Setup(t => t.FindMultiple<ICanResolveTenantId>()).Returns(resolvers);
            container = new Mock<IContainer>();
            container.Setup(c => c.Get<ITypeFinder>()).Returns(type_finder.Object);
            configuration = new TenancyConfiguration();
        };
    }
}
