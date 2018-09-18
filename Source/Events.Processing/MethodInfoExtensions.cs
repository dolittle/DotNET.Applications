/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Reflection;
using Dolittle.Runtime.Events.Processing;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Extension methods for MethodInfo related to Events.Processing
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Extract the <see cref="EventProcessorId" /> from the custom attribute
        /// </summary>
        /// <param name="method">The event processor method info</param>
        /// <returns>The <see cref="EventProcessorId" /></returns>
        public static EventProcessorId EventProcessorId(this MethodInfo method)
        {
            return method.GetCustomAttribute<EventProcessorAttribute>(false)?.Id ?? (EventProcessorId)Guid.Empty;
        }
    }
}