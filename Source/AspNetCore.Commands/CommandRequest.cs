// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.Execution;

namespace Dolittle.AspNetCore.Commands
{
    /// <summary>
    /// Represents a request for executing a command.
    /// </summary>
    public class CommandRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRequest"/> class.
        /// </summary>
        /// <param name="correlationId">The <see cref="CorrelationId"/> representing the request.</param>
        /// <param name="type">The <see cref="ArtifactId"/> representing the type of the Command.</param>
        /// <param name="content">The content of the command.</param>
        public CommandRequest(CorrelationId correlationId, ArtifactId type, IDictionary<string, object> content)
        {
            CorrelationId = correlationId;
            Type = type;
            Content = content;
        }

        /// <summary>
        /// Gets the <see cref="CorrelationId"/> representing the request.
        /// </summary>
        public CorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="ArtifactId"/> representing the type of the Command.
        /// </summary>
        public ArtifactId Type { get; }

        /// <summary>
        /// Gets the content of the command.
        /// </summary>
        public IDictionary<string, object> Content { get; }
    }
}
