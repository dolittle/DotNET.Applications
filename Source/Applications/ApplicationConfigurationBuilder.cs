/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Logging;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationConfigurationBuilder"/>
    /// </summary>
    public class ApplicationConfigurationBuilder : IApplicationConfigurationBuilder
    {
        IApplicationBuilder _applicationBuilder;
        IApplicationStructureMapBuilder _structureMapBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationConfigurationBuilder"/>
        /// </summary>
        /// <param name="name"><see cref="ApplicationName">Name</see> of the application</param>
        public ApplicationConfigurationBuilder(ApplicationName name) : this(new ApplicationBuilder(name), new NullApplicationStructureMapBuilder())
        { 
            Logger.Internal.Trace($"Building application {name}");
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationConfigurationBuilder"/>
        /// </summary>
        /// <param name="name"><see cref="ApplicationName">Name</see> of the application</param>
        /// <param name="structureMapBuilder"><see cref="IApplicationStructureMapBuilder"/> for mapping into <see cref="IApplication"/></param>
        public ApplicationConfigurationBuilder(ApplicationName name, IApplicationStructureMapBuilder structureMapBuilder) : this(new ApplicationBuilder(name), structureMapBuilder)
        { 
            Logger.Internal.Trace($"Building application {name} with a given structure map builder");
        }
        

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationConfigurationBuilder"/>
        /// </summary>
        /// <param name="applicationBuilder"><see cref="IApplicationBuilder"/> for the <see cref="IApplication"/></param>
        /// <param name="structureMapBuilder"><see cref="IApplicationStructureMapBuilder"/> for mapping into <see cref="IApplication"/></param>
        public ApplicationConfigurationBuilder(
            IApplicationBuilder applicationBuilder,
            IApplicationStructureMapBuilder structureMapBuilder)
        {
            _applicationBuilder = applicationBuilder;
            _structureMapBuilder = structureMapBuilder;
        }

        /// <inheritdoc/>
        public IApplicationConfigurationBuilder Application(Func<IApplicationBuilder, IApplicationBuilder> callback)
        {
            var applicationBuilder = callback(_applicationBuilder);
            var applicationConfigurationBuilder = new ApplicationConfigurationBuilder(applicationBuilder, _structureMapBuilder);
            return applicationConfigurationBuilder;
        }


        /// <inheritdoc/>
        public IApplicationConfigurationBuilder StructureMappedTo(Func<IApplicationStructureMapBuilder, IApplicationStructureMapBuilder> callback)
        {
            IApplicationStructureMapBuilder structureMapBuilder = new ApplicationStructureMapBuilder();
            structureMapBuilder = callback(structureMapBuilder);
            var builder = new ApplicationConfigurationBuilder(_applicationBuilder, structureMapBuilder);
            return builder;
        }

        /// <inheritdoc/>
        public (IApplication application, IApplicationStructureMap structureMap) Build()
        {
            var application = _applicationBuilder.Build();
            var applicationStructureMap = _structureMapBuilder.Build(application);
            return (application: application, structureMap: applicationStructureMap);
        }
    }
}
