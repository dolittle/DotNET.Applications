// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
