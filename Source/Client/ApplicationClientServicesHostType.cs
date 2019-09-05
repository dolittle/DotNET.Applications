/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Hosting;

namespace Dolittle.Client
{
    /// <summary>
    /// Represents a <see cref="IRepresentHostType">host type</see> that is for application client communication
    /// </summary>
    /// <remarks>
    /// Application client is considered the channel in which a runtime connects - application client is considered
    /// the representation of the application and is usually represented through an SDK
    /// </remarks>
    public class ApplicationClientServicesHostType : IRepresentHostType
    {
        readonly ClientPort _port;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationClientServicesHostType"/>
        /// </summary>
        /// <param name="port"><see cref="ClientPort"/> to expose</param>
        public ApplicationClientServicesHostType(ClientPort port)
        {
            _port = port;
        }

        /// <inheritdoc/>
        public HostType Identifier => "ApplicationClient";

        /// <inheritdoc/>
        public Type BindingInterface => typeof(ICanBindApplicationClientServices);

        /// <inheritdoc/>
        public HostConfiguration Configuration => new HostConfiguration((int)_port.Value);
    }
}