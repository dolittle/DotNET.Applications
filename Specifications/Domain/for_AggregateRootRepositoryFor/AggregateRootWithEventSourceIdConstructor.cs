/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Domain;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithEventSourceIdConstructor : AggregateRoot
    {
        public AggregateRootWithEventSourceIdConstructor(EventSourceId id) : base(id)
        {

        }
    }
}
