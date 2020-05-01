// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.Events.Filters.Internal;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a manager that deals with registering event filters with the Runtime.
    /// </summary>
    public interface IFilterManager
    {
        /// <summary>
        /// Registers an event filter with the Runtime.
        /// </summary>
        /// <typeparam name="TEventType">The event type that the filter can handle.</typeparam>
        /// <typeparam name="TFilterResult">The type of filter result that the filter returns.</typeparam>
        /// <param name="id">The unique <see cref="FilterId"/> for the filter.</param>
        /// <param name="scope">The <see cref="ScopeId"/> of the scope in the Event Store where the filter will run.</param>
        /// <param name="filter">The implementation of the filter.</param>
        /// <returns>A <see cref="Task"/> representing the execution of the filter.</returns>
        Task Register<TEventType, TFilterResult>(FilterId id, ScopeId scope, ICanFilter<TEventType, TFilterResult> filter)
            where TEventType : IEvent;
    }
}