// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Commands.Coordination;
using Dolittle.Queries.Coordination;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Provides extension methods for <see cref="IEndpointRouteBuilder" />.
    /// </summary>
    public static class EndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Adds a the Dolittle Application Model <see cref="RouteEndpoint">endpoints</see> to the <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
        /// <returns>A <see cref="IEndpointConventionBuilder"/> that can be used to further customize the endpoint.</returns>
        public static IEndpointRouteBuilder MapDolittleApplicationModel(this IEndpointRouteBuilder endpoints)
            => endpoints.MapDolittleApplicationModel(null, null);

        /// <summary>
        /// Adds a the Dolittle Application Model <see cref="RouteEndpoint">endpoints</see> to the <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
        /// <param name="configureCommandCoordinator">An optional <see cref="Action{T}"/> of type <see cref="IEndpointConventionBuilder"/> that will be invoked to configure the <see cref="ICommandCoordinator"/> endpoint.</param>
        /// <param name="configureQueryCoordinator">An optional <see cref="Action{T}"/> of type <see cref="IEndpointConventionBuilder"/> that will be invoked to configure the <see cref="IQueryCoordinator"/> endpoint.</param>
        /// <returns>A <see cref="IEndpointConventionBuilder"/> that can be used to further customize the endpoint.</returns>
        public static IEndpointRouteBuilder MapDolittleApplicationModel(this IEndpointRouteBuilder endpoints, Action<IEndpointConventionBuilder> configureCommandCoordinator = null, Action<IEndpointConventionBuilder> configureQueryCoordinator = null)
        {
            var commandCoordinatorBuilder = endpoints.MapDolittleCommandCoordinator();
            configureCommandCoordinator?.Invoke(commandCoordinatorBuilder);

            var queryCoordinatorBuilder = endpoints.MapDolittleQueryCoordinator();
            configureQueryCoordinator?.Invoke(queryCoordinatorBuilder);

            return endpoints;
        }
    }
}
