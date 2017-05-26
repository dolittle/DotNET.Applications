/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Applications;
using Bifrost.Commands;
using Bifrost.Lifecycle;
using Bifrost.Security;

namespace Bifrost.Web.Commands
{
    public class CommandSecurityService
    {
        readonly ICommandSecurityManager _commandSecurityManager;

        public CommandSecurityService(ICommandSecurityManager commandSecurityManager)
        {
            _commandSecurityManager = commandSecurityManager;
        }

        public AuthorizationResult GetForCommand(IApplicationResourceIdentifier commandType)
        {
            var command = new CommandRequest(TransactionCorrelationId.NotSet, commandType, new Dictionary<string, object>());
            var result = _commandSecurityManager.Authorize(command);
            return result;
        }
    }
}
