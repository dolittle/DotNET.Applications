// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.AspNetCore.Queries;
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
        /// Adds a the <see cref="QueryCoordinator" /> <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
        /// <returns>A <see cref="IEndpointConventionBuilder"/> that can be used to further customize the endpoint.</returns>
        public static IEndpointConventionBuilder MapDolittleQueryCoordinator(this IEndpointRouteBuilder endpoints)
        {
            var queryCoordinator = endpoints.ServiceProvider.GetRequiredService<QueryCoordinator>();
            return endpoints.MapPost("/api/Dolittle/Queries", queryCoordinator.Handle);
        }
    }
}