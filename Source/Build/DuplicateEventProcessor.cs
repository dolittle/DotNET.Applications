/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Build
{
    internal class DuplicateEventProcessor : Exception
    {
        public DuplicateEventProcessor() : base("Found one or more duplications of Event Processors")
        {
        }

        public DuplicateEventProcessor(string message) : base(message)
        {
        }
    }
}