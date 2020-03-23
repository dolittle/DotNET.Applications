// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Applications;

namespace Dolittle.Events.EventHorizon
{
    /// <summary>
    /// Decorates a method to indicate the producer microservice id of an <see cref="IExternalEvent" /> class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ProducerMicroserviceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerMicroserviceAttribute"/> class.
        /// </summary>
        /// <param name="id">The <see cref="Guid" /> identifier.</param>
        public ProducerMicroserviceAttribute(string id)
        {
            Id = Guid.Parse(id);
        }

        /// <summary>
        /// Gets the unique id of the microservices that produces a <see cref="IExternalEvent" />.
        /// </summary>
        public Microservice Id { get; }
    }
}