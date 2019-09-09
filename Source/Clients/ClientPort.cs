/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Clients
{
    /// <summary>
    /// The representation of a TCP socket port for the client
    /// </summary>
    public class ClientPort : ConceptAs<int>
    {
        /// <summary>
        /// Implicitly convert from <see cref="uint"/> to <see cref="ClientPort"/>
        /// </summary>
        /// <param name="value"><see cref="ClientPort"/> as <see cref="int"/></param>
        public static implicit operator ClientPort(int value)
        {
            return new ClientPort { Value = value };
        }
    }

}