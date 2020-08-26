// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Artifacts;
using Dolittle.Commands;
using Dolittle.Commands.Coordination.Runtime;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Microsoft.AspNetCore.Http;
using SdkCommandRequest = Dolittle.Commands.CommandRequest;

namespace Dolittle.AspNetCore.Commands
{
    /// <summary>
    /// Represents an API endpoint for working with <see cref="ICommand">commands</see>.
    /// </summary>
    public class CommandCoordinator
    {
        readonly ICommandCoordinator _commandCoordinator;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCoordinator"/> class.
        /// </summary>
        /// <param name="commandCoordinator">The underlying <see cref="ICommandCoordinator"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public CommandCoordinator(
            ICommandCoordinator commandCoordinator,
            ILogger logger)
        {
            _commandCoordinator = commandCoordinator;
            _logger = logger;
        }

        /// <summary>
        /// Handles a <see cref="CommandRequest" /> from the <see cref="HttpRequest.Body" /> and writes the <see cref="CommandResult" /> to the <see cref="HttpResponse" />.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext" />.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(HttpContext context)
        {
            SdkCommandRequest command = null;
            try
            {
                var request = await context.RequestBodyFromJson<CommandRequest>().ConfigureAwait(false);
                command = new SdkCommandRequest(request.CorrelationId, request.Type, ArtifactGeneration.First, request.Content);
                var result = _commandCoordinator.Handle(command);
                await context.RespondWithStatusCodeAndResult(StatusCodes.Status200OK, result, SerializationOptions.CamelCase).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                _logger.Error(ex, "Could not handle command request");
                await context.RespondWithStatusCodeAndResult(
                    StatusCodes.Status200OK,
                    new CommandResult
                        {
                            Command = command,
                            Exception = ex,
                            ExceptionMessage = ex.Message
                        }).ConfigureAwait(false);
            }
        }
    }
}
