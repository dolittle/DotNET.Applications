// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events;

namespace Dolittle.Machine.Specifications.Events.given
{
    public class AnEvent : IEvent
    {
        public AnEvent(string aString, int anInt)
        {
            AString = aString;
            AnInt = anInt;
        }

        public string AString { get; }

        public int AnInt { get; }
    }
}
