// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Domain;
using Dolittle.Events;

namespace Specs.Feature
{
    public class ImplementationOfAbstractEventSource : AbstractEventSource
    {
        public ImplementationOfAbstractEventSource()
            : base(EventSourceId.New())
        {
        }
    }
}