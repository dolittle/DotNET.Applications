/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Commands;

namespace Bifrost.Web.Commands
{
    public class CommandCoordinatorService
    {
        readonly ICommandCoordinator _commandCoordinator;

        public CommandCoordinatorService(
            ICommandCoordinator commandCoordinator)
        {
            _commandCoordinator = commandCoordinator;
        }

        public CommandResult Handle(CommandRequest command)
        {
            var result = _commandCoordinator.Handle(command);
            return result;
        }

        public IEnumerable<CommandResult> HandleMany(IEnumerable<CommandRequest> commands)
        {
            var results = new List<CommandResult>();
            foreach (var command in commands)
            {
                try
                {
                    results.Add(_commandCoordinator.Handle(command));
                }
                catch (Exception ex)
                {
                    var commandResult = CommandResult.ForCommand(command);
                    commandResult.Exception = ex;
                    return new[] { commandResult };
                }
            }

            return results.ToArray();
        }
    }
}
