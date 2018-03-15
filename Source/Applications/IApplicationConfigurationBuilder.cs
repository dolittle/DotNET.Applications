/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a builder for building <see cref="IApplication"/>
    /// </summary>
    public interface IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Build <see cref="IApplicationBuilder"/> for the <see cref="IApplication"/>
        /// </summary>
        /// <param name="callback">Callback for building</param>
        /// <returns><see cref="IApplicationConfigurationBuilder"/> for continuing building</returns>
        IApplicationConfigurationBuilder Application(Func<IApplicationBuilder, IApplicationBuilder> callback);

        /// <summary>
        /// Build <see cref="IApplicationStructureMapBuilder"/> for the <see cref="IApplication"/>
        /// </summary>
        /// <param name="callback">Callback for building</param>
        /// <returns><see cref="IApplicationConfigurationBuilder"/> for continuing building</returns>
        IApplicationConfigurationBuilder StructureMappedTo(Func<IApplicationStructureMapBuilder, IApplicationStructureMapBuilder> callback);

        /// <summary>
        /// Builds the <see cref="IApplication"/>
        /// </summary>
        /// <returns>A built version of the <see cref="IApplication"/> and <see cref="IApplicationStructureMap"/></returns>
        (IApplication application, IApplicationStructureMap structureMap) Build();
    }
}