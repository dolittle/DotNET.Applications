/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Grpc.Core;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents the details of a client
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets wether or not the application <see cref="Client"/> is connected - useful during booting
        /// </summary>
        /// <value></value>
        public static bool Connected { get; internal set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Client"/>
        /// </summary>
        /// <param name="id"><see cref="ClientId">Id</see> of the client</param>
        /// <param name="port"><see cref="ClientPort">Port</see> the client is exposed on</param>
        /// <param name="callInvoker"><see cref="ChannelBase"/> used to connect to runtime</param>
        public Client(ClientId id, ClientPort port, CallInvoker callInvoker)
        {
            Id = id;
            Port = port;
            CallInvoker = callInvoker;
        }

        /// <summary>
        /// Gets the <see cref="ClientId">unique identifier</see> of the client
        /// </summary>
        public ClientId Id { get; }

        /// <summary>
        /// Gets the <see cref="ClientPort">client port</see> exposed for the runtime to connect to
        /// </summary>
        public ClientPort Port { get; }

        /// <summary>
        /// Gets the <see cref="Channel"/> in which the client uses to connect to the runtime to
        /// </summary>
        public CallInvoker CallInvoker {  get; }
    }
}