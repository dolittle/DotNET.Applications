// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Grpc.Core;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents an implementation of <see cref="IClientFor{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ClientBase"/>.</typeparam>
    public class ClientFor<T> : IClientFor<T>
        where T : ClientBase
    {
        readonly Head _client;
        T _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFor{T}"/> class.
        /// </summary>
        /// <param name="client"><see cref="Head"/> information.</param>
        public ClientFor(Head client)
        {
            _client = client;
        }

        /// <inheritdoc/>
        public T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var type = typeof(T);
                    var constructor = type.GetConstructor(new Type[] { typeof(CallInvoker) });
                    _instance = constructor.Invoke(new[] { _client.CallInvoker }) as T;
                }

                return _instance;
            }
        }
    }
}