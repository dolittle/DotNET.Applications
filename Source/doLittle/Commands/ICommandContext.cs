/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using doLittle.Domain;
using doLittle.Events;
using doLittle.Execution;
using doLittle.Lifecycle;

namespace doLittle.Commands
{
    /// <summary>
    /// Defines a context for a <see cref="ICommand">command</see> passing through
    /// the system
    /// </summary>
    public interface ICommandContext : ITransaction
    {
        /// <summary>
        /// Gets the <see cref="TransactionCorrelationId"/> for the <see cref="ICommandContext"/>
        /// </summary>
        TransactionCorrelationId TransactionCorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="CommandRequest">command</see> the context is for
        /// </summary>
        CommandRequest Command { get; }

        /// <summary>
        /// Gets the <see cref="IExecutionContext"/> for the command
        /// </summary>
        IExecutionContext ExecutionContext { get; }

        /// <summary>
        /// Register an aggregated root for tracking
        /// </summary>
        /// <param name="aggregatedRoot">Aggregated root to track</param>
        void RegisterForTracking(IAggregateRoot aggregatedRoot);

        /// <summary>
        /// Get objects that are being tracked
        /// </summary>
        /// <returns>All tracked objects</returns>
        IEnumerable<IAggregateRoot> GetObjectsBeingTracked();
    }
}