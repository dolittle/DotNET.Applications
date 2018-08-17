/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Build.Topology
{
    internal class InvalidBoundedContextConfiguration : Exception
    {
        internal InvalidBoundedContextConfiguration(string message)
            : base(message) {}   
    }
}