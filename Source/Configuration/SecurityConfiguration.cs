/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Reflection;
using doLittle.Execution;
using doLittle.Security;
using doLittle.Types;
using doLittle.DependencyInversion;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents the configuration for security
    /// </summary>
    public class SecurityConfiguration : ISecurityConfiguration
    {
        /// <inheritdoc/>
        public void Initialize(IContainer container)
        {
            var typeFinder = container.Get<ITypeFinder>();

            var resolverType = typeof(DefaultPrincipalResolver);
            var resolverTypes = typeFinder.FindMultiple<ICanResolvePrincipal>().Where(t => t.GetTypeInfo().Assembly != typeof(SecurityConfiguration).GetTypeInfo().Assembly);
            if (resolverTypes.Count() > 1) throw new MultiplePrincipalResolversFound();
            if (resolverTypes.Count() == 1) resolverType = resolverTypes.First();

            container.Bind<ICanResolvePrincipal>(resolverType);
        }
    }
}
