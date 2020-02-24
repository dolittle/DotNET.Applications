// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dolittle.Domain;
using Dolittle.Events;
using Dolittle.Rules;

#pragma warning disable SA1649

namespace Specs.Feature3
{
    public class TheEventSource : AggregateRoot
    {
        public TheEventSource()
            : base(EventSourceId.New())
        {
        }
    }
}