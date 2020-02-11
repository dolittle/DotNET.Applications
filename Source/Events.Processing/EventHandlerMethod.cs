// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents a handler method for a specific <see cref="IEvent"/>.
    /// </summary>
    public class EventHandlerMethod
    {
        readonly MethodInfo _methodInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethod"/> class.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> for the handle method.</param>
        public EventHandlerMethod(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        /// <summary>
        /// Invoke the method.
        /// </summary>
        /// <param name="handler">The <see cref="ICanHandleEvents">handler</see> instance.</param>
        /// <param name="event"><see cref="CommittedEvent"/> to handle.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Invoke(ICanHandleEvents handler, CommittedEvent @event)
        {
            var result = _methodInfo.Invoke(handler, new[] { @event }) as Task;
            await result.ConfigureAwait(false);
        }
    }
}