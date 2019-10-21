/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Services;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents the runtime services having a client representation
    /// </summary>
    public class ApplicationServices : ICanBindHeadServices
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationServices"/>
        /// </summary>
        public ApplicationServices()
        {
        }

        /// <inheritdoc/>
        public ServiceAspect Aspect => "Client";

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return new Service[0];
        }
    }
}