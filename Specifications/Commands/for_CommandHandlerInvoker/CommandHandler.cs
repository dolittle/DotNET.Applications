/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Runtime.Commands.for_CommandHandlerInvoker
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
