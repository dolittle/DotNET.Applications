/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands.Handling;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker
{
    public class CommandHandler : ICanHandleCommands
    {
        public bool HandleCalled = false;


        public void Handle(Command command)
        {
            HandleCalled = true;
        }
    }
}
