// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Domain;
using Dolittle.Events;

namespace Specs.Feature
{
    public abstract class AbstractEventSource : AggregateRoot
    {
        protected AbstractEventSource(EventSourceId eventSource)
            : base(eventSource)
        {
        }
    }
}