// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Artifacts;
using Dolittle.Events;

namespace Head
{
    [Artifact("e317fb1a-40bd-4630-ba9c-f8f92b90b7f4")]
    public class MyPublicEvent : IPublicEvent
    {
        public string UniqueIdentifier { get; set; }
        public bool Fail { get; set; } = false;
    }
}
