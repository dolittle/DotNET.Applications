/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Commands;
#if(NET461)
using Microsoft.AspNet.SignalR;
#else
using Microsoft.AspNetCore.SignalR;
#endif

namespace Bifrost.Web.Commands
{
    public class CommandCoordinator : Hub
    {
        ICommandCoordinator _commandCoordinator;
        ICommandContextConnectionManager _commandContextConnectionManager;

        public CommandCoordinator(
            ICommandCoordinator commandCoordinator,
            ICommandContextConnectionManager commandContextConnectionManager)
        {
            _commandCoordinator = commandCoordinator;
            _commandContextConnectionManager = commandContextConnectionManager;
        }

        public CommandResult Handle(CommandRequest command)
        {
            try
            {
                _commandContextConnectionManager.Register(Context.ConnectionId, command.CorrelationId);
                var commandResult = _commandCoordinator.Handle(command);
                return commandResult;
            }
            catch (Exception ex)
            {
                return new CommandResult { 
                    Exception = ex, 
                    ExceptionMessage = string.Format("Exception occured of type '{0}' with message '{1}'. StackTrace : {2}",ex.GetType().Name,ex.Message,ex.StackTrace)
                };
            }
        }
    }
}
