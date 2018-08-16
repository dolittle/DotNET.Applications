/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.DependencyInversion;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a very simplementation of <see cref="IContainer"/> that is not capable of resolving
    /// dependencies at all
    /// </summary>
    public class ActivatorContainer : IContainer
    {
        /// <inheritdoc/>
        public T Get<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        /// <inheritdoc/>
        public object Get(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}