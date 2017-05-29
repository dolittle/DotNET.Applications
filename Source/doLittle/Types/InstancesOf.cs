/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using doLittle.Execution;

namespace doLittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IInstancesOf{T}"/>
    /// </summary>
    /// <typeparam name="T">Base type to discover for - must be an abstract class or an interface</typeparam>
    [Singleton]
    public class InstancesOf<T> : IInstancesOf<T>
        where T : class
    {
        readonly IEnumerable<Type> _types;
        readonly IContainer _container;

        /// <summary>
        /// Initalizes an instance of <see cref="IInstancesOf{T}"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> used for discovering types</param>
        /// <param name="container"><see cref="IContainer"/> used for managing instances of the types when needed</param>
        public InstancesOf(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _types = typeDiscoverer.FindMultiple<T>();
            _container = container;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var type in _types) yield return _container.Get(type) as T;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var type in _types) yield return _container.Get(type);
        }
    }
}
