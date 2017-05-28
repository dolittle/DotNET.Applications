/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using doLittle.Commands;
using doLittle.Extensions;
using doLittle.Serialization;
#if (NET461)
using Microsoft.AspNet.SignalR;
#else
using Microsoft.AspNetCore.SignalR;
#endif

namespace doLittle.Web.Commands
{
    public class CommandCoordinator : Hub
    {
        readonly ICommandCoordinator _commandCoordinator;
        readonly ICommandContextConnectionManager _commandContextConnectionManager;
        readonly ISerializer _serializer;

        public CommandCoordinator(
            ICommandCoordinator commandCoordinator,
            ICommandContextConnectionManager commandContextConnectionManager,
            ISerializer serializer)
        {
            _commandCoordinator = commandCoordinator;
            _commandContextConnectionManager = commandContextConnectionManager;
            _serializer = serializer;
        }

        public CommandResult Handle(JsonCommandRequest command)
        {
            try
            {
                var contentAsKeyValues = _serializer.GetKeyValuesFromJson(command.Content).ToDictionary(k => k.Key.ToPascalCase(), k => k.Value);
                var commandRequest = new CommandRequest(command.CorrelationId, command.Type, contentAsKeyValues);

                _commandContextConnectionManager.Register(Context.ConnectionId, command.CorrelationId);
                var commandResult = _commandCoordinator.Handle(commandRequest);
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
