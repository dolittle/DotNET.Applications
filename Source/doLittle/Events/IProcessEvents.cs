/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Conventions;

namespace doLittle.Events
{
    /// <summary>
    /// Defines an event subscriber.
    /// </summary>
    /// <remarks>
    /// An implementation must then implement a Process method that takes the
    /// specific <see cref="IEvent">event</see> you want to be processed.
    /// </remarks>
    public interface IProcessEvents : IConvention
    {
    }
}
