// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.AspNetCore.Commands;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Provides extension methods for <see cref="IEndpointRouteBuilder" />.
    /// </summary>
    public static class EndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Adds a the <see cref="CommandCoordinator" /> <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
        /// <returns>A <see cref="IEndpointConventionBuilder"/> that can be used to further customize the endpoint.</returns>
        public static IEndpointConventionBuilder MapDolittleCommandCoordinator(this IEndpointRouteBuilder endpoints)
        {
            var commandCoordinator = endpoints.ServiceProvider.GetRequiredService<CommandCoordinator>();
            return endpoints.MapPost("/api/Dolittle/Commands", commandCoordinator.Handle);
        }
    }
}
