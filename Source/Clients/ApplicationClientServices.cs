/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Runtime.Application.Grpc.Client;
using Dolittle.Services;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents the runtime services having a client representation
    /// </summary>
    public class ApplicationClientServices : ICanBindApplicationClientServices
    {
        readonly ConnectionStatusService _connectionStatusService;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationClientServices"/>
        /// </summary>
        /// <param name="connectionStatusService">An instance of <see cref="ConnectionStatusService"/></param>
        public ApplicationClientServices(ConnectionStatusService connectionStatusService)
        {
            _connectionStatusService = connectionStatusService;
        }

        /// <inheritdoc/>
        public IEnumerable<Service> BindServices()
        {
            return new[] {
                new Service(_connectionStatusService, ConnectionStatus.BindService(_connectionStatusService), ConnectionStatus.Descriptor)
            };
        }
    }
}