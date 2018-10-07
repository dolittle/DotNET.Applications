/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class SimpleEvent : IEvent
    {
        public string Content { get; set; }
    }
}