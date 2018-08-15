/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Artifacts.Tools
{
    internal class InvalidExcludedNamespaceMap : Exception
    {

        public InvalidExcludedNamespaceMap(string message) : base(message)
        {
        }
    }
}