// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Domain;
using Dolittle.Execution;
using Dolittle.Transactions;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Defines a context for a <see cref="CommandRequest">command</see> passing through the system.
    /// </summary>
    public interface ICommandContext : ITransaction
    {
        /// <summary>
        /// Gets the <see cref="CommandRequest">command</see> the context is for.
        /// </summary>
        CommandRequest Command { get; }

        /// <summary>
        /// Gets the <see cref="ExecutionContext"/> for the command.
        /// </summary>
        ExecutionContext ExecutionContext { get; }

        /// <summary>
        /// Register an <see cref="AggregateRoot">aggregate root</see> for tracking.
        /// </summary>
        /// <param name="aggregateRoot"><see cref="AggregateRoot"/> to be tracked.</param>
        void RegisterForTracking(AggregateRoot aggregateRoot);

        /// <summary>
        /// Get <see cref="AggregateRoot">aggregate roots</see> that are being tracked.
        /// </summary>
        /// <returns>All tracked objects.</returns>
        IEnumerable<AggregateRoot> GetAggregateRootsBeingTracked();
    }
}