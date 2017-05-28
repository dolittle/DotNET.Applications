using System;
using doLittle.Commands;

namespace doLittle.Specs.Commands.for_CommandHandlerInvoker
{
    public class Command : ICommand
    {
        public Guid Id { get; set; }
    }

    public class CommandHandler : IHandleCommands
    {
        public bool HandleCalled = false;


        public void Handle(Command command)
        {
            HandleCalled = true;
        }
    }
}
