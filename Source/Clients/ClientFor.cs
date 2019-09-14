/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Grpc.Core;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="IClientFor{T}"/>
    /// </summary>
    public class ClientFor<T> : IClientFor<T> where T : ClientBase
    {
        readonly Client _client;
        T _instance;

        /// <summary>
        /// Initializes a new instance of <see cref="ClientFor{T}"/>
        /// </summary>
        /// <param name="client"><see cref="Client"/> information</param>
        public ClientFor(Client client)
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
                    var constructor = type.GetConstructor(new Type[]  {  typeof(CallInvoker) });
                    _instance = constructor.Invoke(new [] { _client.CallInvoker }) as T;
                }
                return _instance;
            }
        }
    }
}