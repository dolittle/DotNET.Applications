// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Heads
{
    /// <summary>
    /// The representation of a TCP socket port for the client.
    /// </summary>
    public class HeadPort : ConceptAs<int>
    {
        /// <summary>
        /// Implicitly convert from <see cref="uint"/> to <see cref="HeadPort"/>.
        /// </summary>
        /// <param name="value"><see cref="HeadPort"/> as <see cref="int"/>.</param>
        public static implicit operator HeadPort(int value) => new HeadPort { Value = value };
    }
}