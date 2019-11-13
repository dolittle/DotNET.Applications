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
    public class HeadServices : ICanBindHeadServices
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HeadServices"/>
        /// </summary>
        public HeadServices()
        {
        }

        /// <inheritdoc/>
        public ServiceAspect Aspect => "Runtime";

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return new Service[0];
        }
    }
}