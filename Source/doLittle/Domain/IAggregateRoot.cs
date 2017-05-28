/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Events;
using doLittle.Lifecycle;

namespace doLittle.Domain
{
    /// <summary>
    /// Defines the very basic functionality needed for an aggregated root
    /// </summary>
    public interface IAggregateRoot : IEventSource, ITransaction
    {
    }
}