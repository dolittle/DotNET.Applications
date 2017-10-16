/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Execution;
using doLittle.Types;
using doLittle.Assemblies;
using doLittle.Assemblies.Configuration;
using doLittle.DependencyInversion;

namespace doLittle.Configuration.Defaults
{
    /// <summary>
    /// Represents a <see cref="IDefaultBindings"/>
    /// </summary>
    public class DefaultBindings : IDefaultBindings
    {
        AssembliesConfiguration _assembliesConfiguration;
        IAssemblyProvider _assemblyProvider;
        IContractToImplementorsMap _contractToImplentorsMap;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultBindings"/>
        /// </summary>
        public DefaultBindings(AssembliesConfiguration assembliesConfiguration, IAssemblyProvider assemblyProvider, IContractToImplementorsMap contractToImplentorsMap)
        {
            _assembliesConfiguration = assembliesConfiguration;
            _assemblyProvider = assemblyProvider;
            _contractToImplentorsMap = contractToImplentorsMap;
        }

        /// <inheritdoc/>
        public void Initialize(IContainer container)
        {
            container.Bind(container);
            container.Bind<IContractToImplementorsMap>(_contractToImplentorsMap);
            container.Bind<AssembliesConfiguration>(_assembliesConfiguration);
            container.Bind<IAssemblyProvider>(_assemblyProvider);
            container.Bind<IAssemblies>(typeof(doLittle.Assemblies.Assemblies), BindingLifecycle.Singleton);
            container.Bind<ITypeFinder>(typeof(TypeFinder), BindingLifecycle.Singleton);
        }
    }
}