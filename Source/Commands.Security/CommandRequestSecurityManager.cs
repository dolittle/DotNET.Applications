// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Security;

namespace Dolittle.Commands.Security
{
    /// <summary>
    /// Manages security for a <see cref="CommandRequest" />.
    /// </summary>
    public class CommandRequestSecurityManager : ICommandSecurityManager
    {
        readonly ISecurityManager _securityManager;
        readonly ICommandRequestToCommandConverter _converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRequestSecurityManager"/> class.
        /// </summary>
        /// <param name="securityManager"><see cref="ISecurityManager"/> for forwarding requests related to security to.</param>
        /// <param name="converter"><see cref="ICommandRequestToCommandConverter"/> for converting the Command Request to a Command instance.</param>
        public CommandRequestSecurityManager(ISecurityManager securityManager, ICommandRequestToCommandConverter converter)
        {
            _securityManager = securityManager;
            _converter = converter;
        }

        /// <inheritdoc/>
        public AuthorizationResult Authorize(CommandRequest command)
        {
            return _securityManager.Authorize<HandleCommand>(_converter.Convert(command));
        }
    }
}