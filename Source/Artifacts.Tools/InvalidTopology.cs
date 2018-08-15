/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Artifacts.Tools
{
    internal class InvalidTopology : Exception
    {
        internal InvalidTopology(string message)
            : base(message) {}
    }
}