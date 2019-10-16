/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Services;

namespace Dolittle.Clients
{
    /// <summary>
    /// Defines a system that can bind gRPC services for application client purposes
    /// </summary>
    public interface ICanBindApplicationServices : ICanBindServices
    {
    }
}