/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Dolittle.Applications
{

    /// <summary>
    /// Represents a null implementation of <see cref="IApplicationStructureMapBuilder"/>
    /// </summary>
    public class NullApplicationStructureMapBuilder : IApplicationStructureMapBuilder
    {
        /// <inheritdoc/>
        public IApplicationStructureMap Build(IApplication application)
        {
            return new NullApplicationStructureMap();
        }

        /// <inheritdoc/>
        public IApplicationStructureMapBuilder Include(string format)
        {
            return this;
        }
    }
}
