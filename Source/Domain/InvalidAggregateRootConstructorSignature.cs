// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain
{
    /// <summary>
    /// Gets thrown if an <see cref="AggregateRoot"/> does not follow the convention for expected
    /// signature for the constructor.
    /// </summary>
    /// <remarks>
    /// Expected format is a public constructor with one parameter which is either a <see cref="Guid"/>
    /// or a <see cref="EventSourceId"/>.
    /// </remarks>
    public class InvalidAggregateRootConstructorSignature : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAggregateRootConstructorSignature"/> class.
        /// </summary>
        /// <param name="type">Type of the <see cref="AggregateRoot"/> that is faulty.</param>
        public InvalidAggregateRootConstructorSignature(Type type)
            : base($"Wrong constructor for aggregate root of type '{type.FullName}' - expecting a public constructor taking either a Guid or EventSourceId as the only parameter.")
        {
        }
    }
}
