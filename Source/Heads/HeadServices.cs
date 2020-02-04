// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Services;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents the runtime services having a client representation.
    /// </summary>
    public class HeadServices : ICanBindHeadServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeadServices"/> class.
        /// </summary>
        public HeadServices()
        {
        }

        /// <inheritdoc/>
        public ServiceAspect Aspect => "Runtime";

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return Array.Empty<Service>();
        }
    }
}