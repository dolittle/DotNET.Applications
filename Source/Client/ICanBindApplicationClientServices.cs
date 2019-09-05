/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Hosting;

namespace Dolittle.Client
{
    /// <summary>
    /// Defines a system that can bind gRPC services for application client purposes
    /// </summary>
    public interface ICanBindApplicationClientServices : ICanBindServices
    {
    }
}