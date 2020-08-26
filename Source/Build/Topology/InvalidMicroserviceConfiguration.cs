// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Microservice.Configuration;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Exception that gets thrown when the <see cref="MicroserviceConfiguration"/> that's loaded in is invalid.
    /// </summary>
    public class InvalidMicroserviceConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMicroserviceConfiguration"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public InvalidMicroserviceConfiguration(string description)
            : base(description)
        {
        }
    }
}