// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Domain;
using Dolittle.Events;

#pragma warning disable SA1649
namespace Dolittle.Build.given.ArtifactTypes
{
    public class TheEventSource : AggregateRoot
    {
        public TheEventSource()
            : base(EventSourceId.New())
        {
        }
    }
}
#pragma warning restore SA1649