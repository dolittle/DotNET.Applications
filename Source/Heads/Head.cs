// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        public Head(HeadId id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets a value indicating whether or not the application <see cref="Head"/> is connected - useful during booting.
        /// </summary>
        public static bool Connected { get; internal set; }

        /// <summary>
        /// Gets the <see cref="HeadId">unique identifier</see> of the client.
        /// </summary>
        public HeadId Id { get; }
    }
}