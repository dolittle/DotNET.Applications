// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events;

namespace Dolittle.Commands.Coordination
{
    public class SimpleEvent : IEvent
    {
        public string Content { get; set; }
    }
}