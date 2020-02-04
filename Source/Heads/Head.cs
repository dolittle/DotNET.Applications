// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Grpc.Core;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents the details of a client.
    /// </summary>
    public class Head
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Head"/> class.
        /// </summary>
        /// <param name="id"><see cref="HeadId">Id</see> of the client.</param>
        /// <param name="port"><see cref="HeadPort">Port</see> the client is exposed on.</param>
        /// <param name="callInvoker"><see cref="ChannelBase"/> used to connect to runtime.</param>
        public Head(HeadId id, HeadPort port, CallInvoker callInvoker)
        {
            Id = id;
            Port = port;
            CallInvoker = callInvoker;
        }

        /// <summary>
        /// Gets a value indicating whether or not the application <see cref="Head"/> is connected - useful during booting.
        /// </summary>
        public static bool Connected { get; internal set; }

        /// <summary>
        /// Gets the <see cref="HeadId">unique identifier</see> of the client.
        /// </summary>
        public HeadId Id { get; }

        /// <summary>
        /// Gets the <see cref="HeadPort">client port</see> exposed for the runtime to connect to.
        /// </summary>
        public HeadPort Port { get; }

        /// <summary>
        /// Gets the <see cref="Channel"/> in which the client uses to connect to the runtime to.
        /// </summary>
        public CallInvoker CallInvoker { get; }
    }
}