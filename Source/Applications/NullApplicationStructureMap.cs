/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Strings;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents a null implementation of <see cref="IApplicationStructureMap"/>
    /// </summary>
    public class NullApplicationStructureMap : IApplicationStructureMap
    {
        /// <inheritdoc/>
        public IEnumerable<IStringFormat> Formats => new IStringFormat[0];

        /// <inheritdoc/>
        public bool DoesAnyFitInStructure(IEnumerable<Type> types)
        {
            return false;
        }

        /// <inheritdoc/>
        public bool DoesFitInStructure(Type type)
        {
            return false;
        }

        /// <inheritdoc/>
        public Type GetBestMatchingTypeFor(IEnumerable<Type> types)
        {
            return types.First();
        }
    }
}
