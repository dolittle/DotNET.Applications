/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Execution;

namespace doLittle.Types
{
    /// <summary>
    /// Represents a <see cref="ITypeImporter">ITypeImporter</see>
    /// </summary>
    public class TypeImporter : ITypeImporter
    {
        readonly IContainer _container;
        readonly ITypeDiscoverer _typeDiscoverer;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeImporter">TypeImporter</see>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> that used for creating types</param>
        /// <param name="typeDiscoverer">A <see cref="ITypeDiscoverer">ITypeDiscoverer</see> used for discovering types</param>
        public TypeImporter(IContainer container, ITypeDiscoverer typeDiscoverer)
        {
            _container = container;
            _typeDiscoverer = typeDiscoverer;
        }

        /// <inheritdoc/>
        public T[] ImportMany<T>()
        {
            try
            {
                var types = _typeDiscoverer.FindMultiple<T>();
                ThrowIfTypeCanNotBeImported(typeof(T), types);
                var instances = types.Select(t => (T)_container.Get(t)).ToArray();
                return instances;
            }
            catch (ArgumentException innerException)
            {
                ThrowUnableToImportType(typeof(T),innerException);
            }
            return new T[0];
        }

        /// <inheritdoc/>
        public T Import<T>()
        {
            try
            {
                var singleType = _typeDiscoverer.FindSingle<T>();
                if (null != singleType)
                {
                    var instance = (T)_container.Get(singleType);
                    return instance;
                }
                else
                {
                    ThrowCanNotBeImported(typeof(T));
                }
            }
            catch (ArgumentException innerException)
            {
                ThrowUnableToImportType(typeof(T), innerException);
            }
            return default(T);
        }

        void ThrowUnableToImportType(Type type, ArgumentException innerException)
        {
            throw new ArgumentException(
                string.Format("Can't import type {0}, see inner exception for details", type), innerException);
        }

        void ThrowIfTypeCanNotBeImported(Type type, IEnumerable<Type> types)
        {
            if (types == null) ThrowCanNotBeImported(type);
        }

        void ThrowCanNotBeImported(Type type)
        {
            throw new ArgumentException(
                string.Format("Can't import type {0}, it was not discovered", type));
        }

    }
}