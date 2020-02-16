// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dolittle.Domain;
using Dolittle.Events;
using Dolittle.Rules;

namespace Specs.Feature
{
    public abstract class AbstractEventSource : AggregateRoot
    {
        public AbstractEventSource(EventSourceId eventSource)
            : base(eventSource)
        {
        }
    }
}