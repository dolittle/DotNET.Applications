/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Services;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents the runtime services having a client representation
    /// </summary>
    public class ApplicationClientServices : ICanBindApplicationClientServices
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationClientServices"/>
        /// </summary>
        public ApplicationClientServices()
        {
            
        }

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return new Service[0];
        }
    }
}