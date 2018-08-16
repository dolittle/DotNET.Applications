/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using Dolittle.Types;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IInstancesOf{T}"/> that does not discover - 
    /// but holds a fixed collection of the given type
    /// </summary>
    /// <typeparam name="T">Type of objects to get instances for</typeparam>
    public class FixedInstancesOf<T> : IInstancesOf<T> where T : class
    {
        readonly IEnumerable<T> _collection;

        /// <summary>
        /// Initializes a new instance of <see cref="FixedInstancesOf{T}"/>
        /// </summary>
        /// <param name="collection">Fixed collection of instances of the given type</param>
        public FixedInstancesOf(IEnumerable<T> collection)
        {
            _collection = collection;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}